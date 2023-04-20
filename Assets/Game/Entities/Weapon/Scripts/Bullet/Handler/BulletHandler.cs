using System;
using UnityEngine;

namespace Weapon
{
    public class BulletPool : ObjectPool<Bullet>
    {
    }

    /// <summary> Handles life cycle of bullets </summary>
    public class BulletHandler : MonoBehaviour
    {
        public Action<ShotData> OnShot;
        public Action<HitData> OnHitEnvironment;
        public Action<HitData> OnHitTarget;
        public Action<ExhaustData> OnExhaust;
        public Action OnUnused;

        private BulletTemplateAsset _asset;
        
        public bool IsInit { get; private set; }
        public BulletPool Elements { get; private set; }

    private void OnDestroy ()
        {
            Elements.OnCreateElement -= OnCreateElement;
            Elements.OnDestroyElement -= OnDestroyElement;
        }

        public void Initialize (BulletTemplateAsset asset)
        {
            if (IsInit)
                return;

            _asset = asset;

            Elements = gameObject.AddComponent<BulletPool>();
            Elements.Initialize(_asset.Settings, _asset.Bullet);
            Elements.OnCreateElement += OnCreateElement;
            Elements.OnDestroyElement += OnDestroyElement;

            IsInit = true;
        }

        public void Shoot (ShotData data, IDamageSorce sorce = null, IDamageMediator mediator = null)
        {
            Bullet bullet = Elements.Pool.Get();
            bullet.Fire(data, sorce, mediator);
        }

        private void OnCreateElement (Bullet element) 
        {
            element.OnShot += OnShot;
            element.OnHitEnvironment += OnHitEnvironment;
            element.OnHitTarget += OnHitTarget;
            element.OnExhaust += OnExhaust;
        }

        private void OnDestroyElement (Bullet element) 
        {
            element.OnShot -= OnShot;
            element.OnHitEnvironment -= OnHitEnvironment;
            element.OnHitTarget -= OnHitTarget;
            element.OnExhaust -= OnExhaust;
        }
    }
}
