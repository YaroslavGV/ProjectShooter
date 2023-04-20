using UnityEngine;

namespace Unit
{
    [RequireComponent(typeof(UnitModel))]
    public abstract class CrashDeadEffect : MonoBehaviour
    {
        private UnitModel _unit;

        protected UnitModel Unit => _unit;

        private void Start ()
        {
            _unit = GetComponent<UnitModel>();
            _unit.OnDead += OnUnitDead;
        }

        private void OnDestroy ()
        {
            _unit.OnDead -= OnUnitDead;
        }

        private void OnCollisionEnter (Collision collision)
        {
            if (_unit.IsAlive == false && collision.impulse.normalized.y > 0.3f)
            {
                OnUnitCrash();
                Destroy(gameObject);
            }
        }

        protected virtual void OnUnitDead () { }

        protected virtual void OnUnitCrash () { }
    }
}