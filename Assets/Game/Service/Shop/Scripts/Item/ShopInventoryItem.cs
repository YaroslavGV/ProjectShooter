using UnityEngine;
using InventorySystem;

namespace Shop
{
    [CreateAssetMenu(fileName = "InventoryItem", menuName = "Shop/Item/Inventory")]
    public class ShopInventoryItem : ShopItem
    {
        [SerializeField] private Item _item;

        public override Sprite Icon => _item.Icon;
        public Item Item => _item;
    }
}