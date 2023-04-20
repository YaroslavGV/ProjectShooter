using UnityEngine;

namespace Unit.Skin
{
    public abstract class ShootAgent : MonoBehaviour
    {
        [SerializeField] private Key _targetPointKey;

        public string Key => _targetPointKey.Name;

        public virtual void StartShooting () { }

        public virtual void StopShooting () { }

        public virtual void Shoot () { }
    }
}