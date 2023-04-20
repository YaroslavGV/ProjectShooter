using System;

namespace Weapon
{
    [Serializable]
    public struct BustFire
    {
        public bool enable;
        public int lenght;
        public float delay;

        public BustFire (bool enable, int lenght, float delay)
        {
            this.enable = enable;
            this.lenght = lenght;
            this.delay = delay;
        }

        public static BustFire Default => new BustFire(false, 3, 0.1f);
    }

    [Serializable]
    public struct ShootingSettings
    {
        public float fireRate;
        public BustFire burst;

        public ShootingSettings (float fireRate, BustFire burst)
        {
            this.fireRate = fireRate;
            this.burst = burst;
        }

        public static ShootingSettings Default => new ShootingSettings(1, BustFire.Default);
    }

    [Serializable]
    public struct ShotSettings
    {
        public float scatter;
        public int pallet;

        public ShotSettings (float scatter, int pallet)
        {
            this.scatter = scatter;
            this.pallet = pallet;
        }

        public static ShotSettings Default => new ShotSettings(5, 1);
    }

    [Serializable]
    public struct AutofireSettings
    {
        public ShootingSettings shooting;
        public ShotSettings shot;

        public AutofireSettings (ShootingSettings shooting, ShotSettings shot) : this()
        {
            this.shooting = shooting;
            this.shot = shot;
        }

        public static AutofireSettings Default => new AutofireSettings(ShootingSettings.Default, ShotSettings.Default);
    }
}