using System;
using UnityEngine;

namespace Weapon
{
    public class BulletEffectPool : ObjectPool<BulletEffect>
    {
    }

    /// <summary> Handles the life cycle of one of the bullet effects </summary>
    public class BulletEffectHandler : MonoBehaviour
    {
        private BulletEffectData _data;
        
        public BulletEffectPool Elements { get; private set; }

        public void Initialize (BulletEffectData data)
        {
            _data = data;
            Elements = gameObject.AddComponent<BulletEffectPool>();
            Elements.Initialize(data.poolSettings, data.effect);
        }

        public void OnShot (ShotData data)
        {
            if (_data.eventType == EffectEventType.Shot)
                Play(data.point, data.point.position, data.point.rotation);
        }

        public void OnHitEnvironment (HitData data)
        {
            if (_data.eventType == EffectEventType.HitEnvironment)
                Play(data.collider.transform, data.collisionPoint, Quaternion.LookRotation(data.surfaceNormal, Vector3.up));
        }

        public void OnHitTarget (HitData data)
        {
            if (_data.eventType == EffectEventType.HitTarget)
                Play(data.collider.transform, data.collisionPoint, Quaternion.LookRotation(data.surfaceNormal, Vector3.up));
        }

        public void OnExhaust (ExhaustData data)
        {
            if (_data.eventType == EffectEventType.Exhaust)
                Play(null, data.position, Quaternion.LookRotation(data.vector, Vector3.up));
        }

        public void Play (Transform parent, Vector3 point, Quaternion rotation)
        {
            BulletEffect effect = Elements.Pool.Get();
            effect.transform.position = point;
            effect.transform.rotation = rotation;
            if (_data.changeParent && parent != null)
                effect.transform.SetParent(parent);
            effect.Play();
        }
    }
}
