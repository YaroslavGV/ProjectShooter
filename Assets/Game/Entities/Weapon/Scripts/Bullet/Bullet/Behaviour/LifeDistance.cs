using System;

namespace Weapon
{
    public class LifeDistance
    {
        public Action LifeOver;
        private float _distance = 32;

        public float Remainder { get; private set; }

        public void SetDistance (float distance) => _distance = distance;

        public void SpendDistance (float distance)
        {
            Remainder -= distance;
            if (Remainder <= 0)
                LifeOver?.Invoke();
        }

        public void Reset () => Remainder = _distance;
    }
}