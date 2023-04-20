using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Weapon
{
    public class BulletEffect : MonoBehaviour, IConsumable<BulletEffect>
    {
        public UnityEvent OnPlay;
        [SerializeField] private float _conusmeAfter = 1;
        private Action<BulletEffect> _consumeCallback;
        
        public void SetConsumeCallback (Action<BulletEffect> callback) => _consumeCallback = callback;

        public void Play ()
        {
            OnPlay?.Invoke();
            StopAllCoroutines();
            StartCoroutine(ConsumeDelay());
        }

        private IEnumerator ConsumeDelay ()
        {
            yield return new WaitForSeconds(_conusmeAfter);
            _consumeCallback?.Invoke(this);
        }
    }
}
