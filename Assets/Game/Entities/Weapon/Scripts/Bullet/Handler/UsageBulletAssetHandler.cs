using System;
using System.Collections.Generic;

namespace Weapon
{
    /// <summary> Keeps track of usage BulletAssetHandler </summary>
    public class UsageBulletAssetHandler : BulletAssetHandler
    {
        public Action<UsageBulletAssetHandler> OnUnused;
        private List<IBulletAssetUser> _users;

        public bool IsUsed { get; private set; }

        public void AddUser (IBulletAssetUser user)
        {
            _users.Add(user);
            IsUsed = true;
        }

        protected override void OnDestroy ()
        {
            base.OnDestroy();
            BulletHandler.Elements.OnTakeElement -= OnTakeBullet;
            BulletHandler.Elements.OnReturnElement -= OnReturnBullet;

            foreach (BulletEffectHandler effect in EffectHandlers)
            {
                effect.Elements.OnTakeElement -= OnTakeEffect;
                effect.Elements.OnReturnElement -= OnReturnEffect;
            }
        }

        public override void Initialize (BulletTemplateAsset asset)
        {
            base.Initialize(asset);
            
            BulletHandler.Elements.OnTakeElement += OnTakeBullet;
            BulletHandler.Elements.OnReturnElement += OnReturnBullet;

            foreach (BulletEffectHandler effect in EffectHandlers)
            {
                effect.Elements.OnTakeElement += OnTakeEffect;
                effect.Elements.OnReturnElement += OnReturnEffect;
            }

            _users = new List<IBulletAssetUser>();
        }

        public void RemoveUser (IBulletAssetUser user)
        {
            if (_users.Remove(user))
                CheckUnused();
        }

        private void OnTakeBullet (Bullet bullet) => IsUsed = true;

        private void OnReturnBullet (Bullet bullet) => CheckUnused();

        private void OnTakeEffect (BulletEffect effect) => IsUsed = true;

        private void OnReturnEffect (BulletEffect effect) => CheckUnused();

        private void CheckUnused ()
        {
            IsUsed = _users.Count > 0 || BulletHandler.Elements.UsedElements > 0 || HaveUsedEffects();
            if (IsUsed == false)
                OnUnused?.Invoke(this);
        }

        private bool HaveUsedEffects ()
        {
            foreach (BulletEffectHandler effect in EffectHandlers)
                if (effect.Elements.UsedElements > 0)
                    return true;
            return false;
        }
    }
}
