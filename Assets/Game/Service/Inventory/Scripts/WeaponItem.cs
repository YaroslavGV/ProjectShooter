using UnityEngine;
using Weapon;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "WeaponItem", menuName = "Inventory/WeaponItem")]
    public class WeaponItem : Item
    {
        [SerializeField] private WeaponModel _model;

        public WeaponType Type => _model.Type;
        public WeaponModel Model => _model;
    }
}