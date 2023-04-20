using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    /// <summary> Create and handles bullet and effects handlers </summary>
    public class BulletAssetHandler : MonoBehaviour
    {
        private BulletHandler _handler;
        private BulletEffectHandler[] _effects;

        public BulletTemplateAsset Asset { get; private set; }
        public BulletHandler BulletHandler => _handler;
        public IEnumerable<BulletEffectHandler> EffectHandlers => _effects;

        protected virtual void OnDestroy ()
        {
            foreach (BulletEffectHandler effect in _effects)
            {
                _handler.OnShot -= effect.OnShot;
                _handler.OnHitEnvironment -= effect.OnHitEnvironment;
                _handler.OnHitTarget -= effect.OnHitTarget;
                _handler.OnExhaust -= effect.OnExhaust;
            }
        }

        public virtual void Initialize (BulletTemplateAsset asset)
        {
            Asset = asset;
            
            _handler = gameObject.AddComponent<BulletHandler>();
            _handler.Initialize(Asset);

            _effects = new BulletEffectHandler[Asset.EffectsCount];
            int e = 0;
            foreach (BulletEffectData effectData in Asset.Effects)
            {
                BulletEffectHandler effect = gameObject.AddComponent<BulletEffectHandler>();
                effect.Initialize(effectData);

                _handler.OnShot += effect.OnShot;
                _handler.OnHitEnvironment += effect.OnHitEnvironment;
                _handler.OnHitTarget += effect.OnHitTarget;
                _handler.OnExhaust += effect.OnExhaust;

                _effects[e++] = effect;
            }
        }
    }
}
