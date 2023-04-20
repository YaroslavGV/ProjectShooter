using UnityEngine;

namespace Weapon
{
    [RequireComponent(typeof(WeaponModel))]
    public class TestWeapon : MonoBehaviour
    {
        [SerializeField] private bool _shootOnStart = true;
        private WeaponModel _weapon;

        private void Start ()
        {
            _weapon = GetComponent<WeaponModel>();
            _weapon.Initialize();
            if (_shootOnStart)
                StartShooting();
        }

        [ContextMenu("StartShooting")]
        public void StartShooting () => _weapon.StartShooting();

        [ContextMenu("StopShooting")]
        public void StopShooting () => _weapon.StopShooting();
    }
}