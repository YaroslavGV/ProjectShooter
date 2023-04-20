using System;
using System.Collections.Generic;

namespace StatSystem
{
    public class Stat
    {
        public event Action Changed;
        public readonly StatType Type;
        public readonly float BaseValue;
        private readonly List<StatModifier> _modifiers;

        private float _value;
        private float _bonusValue;
        private float _multiplayert;
        private float _totalMultiplayert;
        
        public Stat (StatType type, float baseValue) 
        {
            Type = type;
            BaseValue = baseValue;
            _modifiers = new List<StatModifier>();
            CalculateValue();
        }

        public Stat (StatValue stat) : this(stat.type, stat.value) { }

        public float value => _value;
        public float bonusValue => _bonusValue;
        public float multiplayert => _multiplayert;
        public float totalMultiplayert => _totalMultiplayert;

        public override string ToString () => 
            string.Format("{0}: ({1} + {2}) * {3} * {4} = {5}", Type, BaseValue, _bonusValue, multiplayert, totalMultiplayert, value);

        public bool AddModifier (StatModifier modifire) 
        {
            if (Type == modifire.type && _modifiers.Contains(modifire) == false)
            {
                _modifiers.Add(modifire);
                CalculateValue();
                return true;
            }
            return false;
        }

        public bool RemoveModifier (StatModifier modifire) 
        {
            bool result = _modifiers.Remove(modifire);
            if (result)
                CalculateValue();
            return result;
        }

        public bool RemoveModifiersFromSorce (object sorce) 
        {
            bool result = _modifiers.RemoveAll(m => m is OwnedStatModifire om && om.sorce == sorce) > 0;
            if (result)
                CalculateValue();
            return result;
        }

        private void CalculateValue () 
        {
            _bonusValue = 0;
            _multiplayert = 1;
            _totalMultiplayert = 1;
            foreach (var modifier in _modifiers) 
            {
                if (modifier.species == ModifierSpecies.Bonus)
                    _bonusValue += modifier.value;
                else if (modifier.species == ModifierSpecies.Multiplayer)
                    _multiplayert += modifier.value;
                else if (modifier.species == ModifierSpecies.TotalMultiplayer)
                    _totalMultiplayert += modifier.value;
            }
            _value = (BaseValue + _bonusValue) * _multiplayert * _totalMultiplayert;
            if (Changed != null)
                Changed.Invoke();
        }
    }
}