using System;
using UnityEngine;

namespace Weapon
{
    [Serializable]
    public class LifeTime
    {
        public Action LifeOver;
        [SerializeField] private float _time = 5;
        private float _elapsed;

        public void TimePass (float delta)
        {
            _elapsed += delta;
            if (_elapsed >= _time)
                LifeOver?.Invoke();
        }

        public void Reset () => _elapsed = 0;
    }
}