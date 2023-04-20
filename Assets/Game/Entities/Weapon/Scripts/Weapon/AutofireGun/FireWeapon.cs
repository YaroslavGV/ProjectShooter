using UnityEngine;
using Zenject;

namespace Weapon
{
    public interface IBulletAssetUser
    {
        public BulletTemplateAsset Asset { get; }
    }

    public class FireWeapon : WeaponModel, IBulletAssetUser
    {
        [SerializeField] private AutofireSettings _settings = AutofireSettings.Default;
        [SerializeField] private BulletTemplateAsset _bulletAsset;
        [Tooltip("use self transform if point is null")]
        [SerializeField] private Transform _shotPoint;
        [SerializeField] private bool _initializeOnStart = true;
        private SceneBullets _sceneBullets;
        private BulletHandler _bullets;
        private ShootingHandler _shooting;
        private ShootHandler _shoot;
        
        public override bool IsInit => _shooting != null;
        public BulletTemplateAsset Asset => _bulletAsset;
        public override bool IsShooting => _shooting.IsShooting;
        public override WeaponType Type => WeaponType.Primary;
        public AutofireSettings Settings => _settings;
        public ShootingHandler Shooting => _shooting;
        public ShootHandler Shoot => _shoot;

        private void Start ()
        {
            if (_initializeOnStart)
                Initialize();
        }

        private void OnDestroy ()
        {
            _shooting.OnTrigger -= _shoot.ApplyShot;
            _shoot.OnShoot -= DoShoot;
            _sceneBullets.RemoveUser(this);
        }

        [Inject]
        public void SetSceneBullets (SceneBullets bullets)
        {
            _sceneBullets = bullets;
        }

        public override void Initialize ()
        {
            if (IsInit)
                return;

            _bullets = _sceneBullets.AddUser(this);
            
            _shooting = new ShootingHandler(_settings.shooting, this);
            Transform shotPoint = _shotPoint != null ? _shotPoint : transform;
            _shoot = new ShootHandler(_settings.shot, shotPoint);

            _shooting.OnTrigger += _shoot.ApplyShot;
            _shoot.OnShoot += DoShoot;
        }

        public override void SetShooting (bool isShooting)
        {
            if (isShooting)
                StartShooting();
            else
                StopShooting();
        }

        public override void StartShooting () => _shooting.StartShooting();

        public override void StopShooting () => _shooting.StopShooting();

        private void DoShoot (ShotData data) 
        {
            _bullets.Shoot(data, Sorce, this);
            OnShoot?.Invoke(this);
        }
    }
}