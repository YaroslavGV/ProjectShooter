using System;
using System.Linq;
using UnityEngine;
using StatSystem;
using FractionSystem;
using Weapon;

namespace Unit
{
    using Skin;

    [RequireComponent(typeof(Rigidbody))]
    public class UnitModel : MonoBehaviour, IFractionMember, IDamageSorce, IDamageable, IDamageReportReceiver
    {
        public Action<DamageReport> OnTakeDamage;
        public Action<DamageReport> OnDealDamage;
        public Action OnDead;
        [Tooltip("Order of modul initialize")]
        [SerializeField] private UnitModelModul[] _moduls;
        private DamageProducer _damageProducer;
        private DamageHandler _damageHandler;

        public bool IsInit { get; private set; }
        public UnitProfile Profile { get; private set; }
        public IFraction Fraction { get; private set; }
        public StatGroup Stats { get; private set; }
        public UnitSkin Skin { get; private set; }
        public Rigidbody Body { get; private set; }
        public FloatingValue Health { get; private set; }
        public InputValues Inputs { get; set; }
        public bool IsAlive => _damageHandler.IsAlive;
        
        private void Update ()
        {
            if (IsInit && IsAlive)
                foreach (UnitModelModul modul in _moduls)
                    modul.Tick();
        }

        public override string ToString ()
        {
            return string.Format("Unit {0} {1}", name, Fraction.Name);
        }

        public void Initialize (UnitProfile profile, Fraction fraction)
        {
            if (IsInit)
                return;

            Profile = profile;
            Fraction = fraction;

            Stats = new StatGroup(Profile.baseStats);
            Skin = Instantiate(Profile.skin, transform);
            Body = GetComponent<Rigidbody>();
            
            _damageProducer = new DamageProducer(this);
            _damageHandler = new DamageHandler(this);

            var health = Stats.GetStat(StatType.Health);
            Health = new FloatingValue(health.value);
            Health.SetupMaxValue();

            foreach (UnitModelModul modul in _moduls)
                modul.Initialize(this);

            IsInit = true;
        }

        public T GetModul<T> () where T : UnitModelModul
        {
            Type targetType = typeof(T);
            var modul = _moduls.FirstOrDefault(m => targetType.IsAssignableFrom(m.GetType()));
            if (modul == null)
                Debug.LogWarning(targetType + " modul is missing");
            else if (modul.IsInit == false)
                Debug.LogWarning(targetType + " modul is not init");
            return (T)modul;
        }

        public bool CanAttack (IDamageable target) => _damageProducer.CanAttack(target);

        public void DoDamage (IDamageable target, IDamageMediator mediator) => _damageProducer.DoDamage(target, mediator);

        public void ApplyDamage (Damage damage) => _damageHandler.ApplyDamage(damage);

        public void AcceptDealDamageReport (DamageReport report)
        {
            OnDealDamage?.Invoke(report);
        }

        public void AcceptTakeDamageReport (DamageReport report)
        {
            OnTakeDamage?.Invoke(report);
            if (IsAlive == false)
                Dead();
        }

        private void Dead ()
        {
            foreach (UnitModelModul modul in _moduls)
                modul.OnDead();
            OnDead?.Invoke();
        }
    }
}