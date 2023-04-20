using System;
using System.Collections;
using UnityEngine;

namespace Weapon
{
    /// <summary> Triggered shot event using fire rate and burst settings when shooting </summary>
    public class ShootingHandler
    {
        public Action OnTrigger;
        private readonly ShootingSettings _settings;
        private readonly MonoBehaviour _coroutineHandler;
        private IEnumerator _process;

        public ShootingSettings Settings => _settings;
        public bool IsShooting { get; private set; }

        public ShootingHandler (ShootingSettings settings, MonoBehaviour coroutineHandler)
        {
            _settings = settings;
            _coroutineHandler = coroutineHandler;
        }

        public void StartShooting ()
        {
            IsShooting = true;
            if (_process != null)
                return;
            _process = ShootProcess();
            _coroutineHandler.StartCoroutine(_process);
        }

        public void StopShooting ()
        {
            IsShooting = false;
        }

        private IEnumerator ShootProcess ()
        {
            float elapsed = _settings.fireRate;
            while (true)
            {
                if (elapsed >= _settings.fireRate)
                {
                    if (IsShooting)
                    {
                        elapsed -= _settings.fireRate;
                        if (_settings.burst.enable)
                            _coroutineHandler.StartCoroutine(BurstProcess());
                        else
                            OnTrigger?.Invoke();
                    }
                    else
                    {
                        _process = null;
                        yield break;
                    }
                }
                elapsed += Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator BurstProcess ()
        {
            int count = 0;
            while (count < _settings.burst.lenght)
            {
                OnTrigger?.Invoke();
                count++;
                yield return new WaitForSeconds(_settings.burst.delay);
            }
        }
    }
}