using UnityEngine;
using UnityEngine.Events;

namespace Weapon
{
    [RequireComponent(typeof(Bullet))]
    public class BulletEvents : MonoBehaviour
    {
        public UnityEvent OnShot;
        public UnityEvent OnHitEnvironment;
        public UnityEvent OnHitTarget;
        public UnityEvent OnExhaust;
        private Bullet _bullet;

        private void OnEnable ()
        {
            _bullet = GetComponent<Bullet>();

            _bullet.OnShot += Shot;
            _bullet.OnHitEnvironment += HitEnvironment;
            _bullet.OnHitTarget += HitTarget;
            _bullet.OnExhaust += Exhaust;
        }

        private void OnDisable ()
        {
            _bullet.OnShot -= Shot;
            _bullet.OnHitEnvironment -= HitEnvironment;
            _bullet.OnHitTarget -= HitTarget;
            _bullet.OnExhaust -= Exhaust;
        }

        private void Shot (ShotData data) => OnShot?.Invoke();
        private void HitEnvironment (HitData data) => OnHitEnvironment?.Invoke();
        private void HitTarget (HitData data) => OnHitTarget?.Invoke();
        private void Exhaust (ExhaustData data) => OnExhaust?.Invoke();
    }
}