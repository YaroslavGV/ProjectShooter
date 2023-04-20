using UnityEngine;

namespace Weapon
{
    public class TestSingleBullet : TestBullet
    {
        [SerializeField] private Bullet _bullet;

        protected override void Shot (ShotData data)
        {
            Bullet bullet = Instantiate(_bullet);
            bullet.Fire(data);
            bullet.SetConsumeCallback(OnConsume);
        }

        private void OnConsume (Bullet bullet) => Destroy(bullet);
    }
}