using System;

namespace Weapon
{
    [Serializable]
    public struct BulletSettings
    {
        public float speed;
        public float maxDistance;
        public bool piercing;
        public bool ricochet;

        public BulletSettings (float speed, float maxDistance, bool piercing = false, bool ricochet = false)
        {
            this.speed = speed;
            this.maxDistance = maxDistance;
            this.piercing = piercing;
            this.ricochet = ricochet;
        }

        public static BulletSettings Default => new BulletSettings(16, 32);
    }
}