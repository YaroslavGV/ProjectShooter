using System;
using System.Linq;
using System.Collections.Generic;

namespace StatSystem
{
    public class StatGroup
    {
        /// <summary> Adds new stat if it is missing when requested </summary>
        public readonly bool AutoAdd;
        private readonly Dictionary<StatType, Stat> _stats;

        public StatGroup (IEnumerable<StatValue> baseValues, bool autoAdd = true)
        {
            AutoAdd = autoAdd;
            _stats = new Dictionary<StatType, Stat>();
            foreach (var baseValue in baseValues)
                AddStat(baseValue);
        }

        public StatGroup (StatValues stats, bool autoAdd = true) : this(stats.values, autoAdd)
        {
        }

        public override string ToString () => string.Join(Environment.NewLine, _stats.Select(s => s.Value.ToString()));

        public Stat GetStat (StatType type)
        {
            if (_stats.ContainsKey(type))
                return _stats[type];
            if (AutoAdd)
                return AddStat(type, 0);
            throw new ArgumentException("Group is not contain stat with "+type.ToString()+" type");
        }

        public bool AddModifier (StatModifier modifier) 
        {
            if (_stats.ContainsKey(modifier.type))
            {
                return _stats[modifier.type].AddModifier(modifier);
            }
            else
            {
                Stat newStat = new Stat(modifier.type, 0);
                _stats.Add(modifier.type, newStat);
                return AddModifier(modifier);
            }
        }

        public void AddModifiers (IEnumerable<StatModifier> modifiers)
        {
            foreach (var modifier in modifiers)
                AddModifier(modifier);
        }

        public bool RemoveModifier (StatModifier modifier) 
        {
            if (_stats.ContainsKey(modifier.type))
                return _stats[modifier.type].RemoveModifier(modifier);
            return false;
        }

        public void RemoveModifiersFromSorce (object sorce) 
        {
            foreach (var stat in _stats)
                stat.Value.RemoveModifiersFromSorce(sorce);
        }

        private Stat AddStat (StatValue baseValue) =>
            AddStat(baseValue.type, baseValue.value);

        private Stat AddStat (StatType type, float baseValue)
        {
            if (_stats.ContainsKey(type))
                return _stats[type];
            Stat newStat = new Stat(type, baseValue);
            _stats.Add(newStat.Type, newStat);
            return newStat;
        }
    }
}
