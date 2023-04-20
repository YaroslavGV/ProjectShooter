using System;
using UnityEngine;

namespace Unit
{
    public class FloatingValue
    {
        public Action OnChange;
        private float _max;
        private float _value;

        public FloatingValue (float max)
        {
            if (max <= 0)
                throw NotPositiveMax;
            _max = max;
        }

        public float RelativeValue => _value / _max;

        public float Max
        {
            get => _max;
            set
            {
                if (value <= 0)
                    throw NotPositiveMax;
                if (_max == value)
                    return;
                float rate = _value / _max;
                _max = value;
                Value = _max * rate;
            }
        }

        public float Value
        {
            get => _value;
            set
            {
                float cValue = Mathf.Clamp(value, 0, _max);
                if (_value == cValue)
                    return;
                _value = cValue;
                OnChange?.Invoke();
            }
        }

        public float SetupMaxValue () => Value = _max;

        private Exception NotPositiveMax => throw new Exception("Max value must be positive");
    }
}