using UnityEngine;

namespace Unit
{
    public class FireCrashDeadEffect : CrashDeadEffect
    {
        [SerializeField] private ParticleSystem _fireTemplate;
        [SerializeField] private ParticleSystem _explosionTemplate;
        private ParticleSystem _fire;

        protected override void OnUnitDead ()
        {
            Rigidbody body = Unit.Body;
            body.useGravity = true;
            body.constraints = RigidbodyConstraints.None;
            _fire = Instantiate(_fireTemplate, transform);
        }

        protected override void OnUnitCrash ()
        {
            _fire.transform.SetParent(null);
            _fire.Stop();
            Instantiate(_explosionTemplate, transform.position, Quaternion.identity);
        }
    }
}
