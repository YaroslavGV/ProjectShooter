using UnityEngine;
using StatSystem;

namespace Unit
{
    public class DamageHandler : IDamageable
    {
        private readonly UnitModel _unit;
        
        public DamageHandler (UnitModel target) => _unit = target;

        public bool IsAlive => _unit.Health.Value > 0;

        public void ApplyDamage (Damage damage)
        {
            if (IsAlive == false)
                return;
            var Stats = _unit.Stats;
            var health = Stats.GetStat(StatType.Health);
            var armor = Stats.GetStat(StatType.Armor);

            float mimicHealth = health.value+armor.value;
            float damagedHealth = mimicHealth-damage.value;
            float rate = 1-damagedHealth/mimicHealth;
            float resultDamage = health.value*rate;

            _unit.Health.Value -= resultDamage;

            DamageReport report = new DamageReport(damage, _unit, resultDamage);
            report.SendToReceivers();
        }
    }
}
