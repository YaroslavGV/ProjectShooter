using System;
using UnityEngine;

namespace Weapon
{
    /// <summary> Generates ShotData when shot by applying scatter and pallet setting </summary>
    public class ShootHandler
    {
        public Action<ShotData> OnShoot;
        readonly private ShotSettings _settings;
        readonly private Transform _shotPoint;
        
        public ShootHandler (ShotSettings settings, Transform shotPoint) 
        {
            _settings = settings;
            _shotPoint = shotPoint;
        }

        public void ApplyShot ()
        {
            for (int i = 0; i < _settings.pallet; i++)
            {
                Vector3 position = _shotPoint.position;
                Vector3 direction = GetDirection(_shotPoint.forward, _settings.scatter);
                OnShoot?.Invoke(new ShotData(_shotPoint, direction));
            }
        }

        private Vector3 GetDirection (Vector3 direction, float scatterAngle) =>
            Vector3.Slerp(direction, UnityEngine.Random.insideUnitSphere, scatterAngle / 180f).normalized;
    }
}