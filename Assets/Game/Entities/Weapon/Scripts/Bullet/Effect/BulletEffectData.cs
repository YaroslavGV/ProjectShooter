using System;

namespace Weapon
{
    public enum EffectEventType { Shot, HitEnvironment, HitTarget, Exhaust }

    [Serializable]
    public struct BulletEffectData
    {
        public BulletEffect effect;
        public EffectEventType eventType;
        public bool changeParent;
        public PoolSettings poolSettings;
    }
}
