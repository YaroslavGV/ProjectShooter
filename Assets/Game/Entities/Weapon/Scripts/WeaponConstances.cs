using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "WeaponConstances", menuName = "Weapon/Constances")]
    public class WeaponConstances : ScriptableObject
    {
        [Tooltip("Aim magnitude value")]
        [SerializeField] private float _shootTrigger = 0.9f;

        public float ShootTrigger => _shootTrigger;
    }
}