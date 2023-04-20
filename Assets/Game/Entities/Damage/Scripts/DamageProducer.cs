using UnityEngine;
using StatSystem;
using FractionSystem;
using Weapon;

namespace Unit
{
    public class DamageProducer : IDamageSorce
    {
        private readonly UnitModel _unit;

        public DamageProducer (UnitModel target) => _unit = target;

        public bool CanAttack (IDamageable target)
        {
            bool can = true;
            if (target is IFractionMember fTaret && _unit.Fraction == fTaret.Fraction)
                can = false;
            return can;
        }

        public void DoDamage (IDamageable target, IDamageMediator mediator)
        {
            StatGroup stats = _unit.Stats;
            Stat sDamage = stats.GetStat(StatType.AttackDamage);
            Stat sCritChance = stats.GetStat(StatType.CritialChance);
            Stat sCritPower = stats.GetStat(StatType.CriticalPower);

            bool isCrit = false;
            float damage = sDamage.value;
            float critChange = sCritChance.value;
            float critPower = sCritPower.value;

            float damageAmount = damage * mediator.DamageFactor;
            if (critChange > 0 && critPower > 1)
            {
                isCrit = critChange > Random.Range(0f, 1f);
                if (isCrit)
                    damageAmount *= critPower;
            }
            target.ApplyDamage(new Damage(_unit, damageAmount, isCrit));
        }
    }
}
