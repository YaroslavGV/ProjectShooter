using System;
using UnityEngine;

namespace Weapon
{
    public enum WeaponType { Primary, Special }

    public abstract class WeaponModel : MonoBehaviour, IDamageMediator
    {
        [SerializeField] private float _damageFactor = 1;
        [SerializeField] private float _targetRange = 6;
        private IDamageSorce _sorce;

        public virtual bool IsInit => true;
        public virtual Action<WeaponModel> OnShoot { get; set; }
        public abstract WeaponType Type { get; }
        public abstract bool IsShooting { get; }
        protected IDamageSorce Sorce => _sorce;
        public float DamageFactor => _damageFactor;
        public float TargetRange => _targetRange;

        public void SetDamageSorce (IDamageSorce sorce) => _sorce = sorce;

        public virtual void Initialize () { }
        public abstract void SetShooting (bool isShooting);
        public abstract void StartShooting ();
        public abstract void StopShooting ();
    }
}