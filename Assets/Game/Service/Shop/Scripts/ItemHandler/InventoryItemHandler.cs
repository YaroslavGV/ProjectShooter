using System;
using InventorySystem;

namespace Shop
{
    public class InventoryItemHandler : ShopItemHandler
    {
        public readonly Inventory _inventory;

        public InventoryItemHandler (Inventory inventory)
        {
            _inventory = inventory;
        }

        public override Type ProcessedType => typeof(ShopInventoryItem);

        public override bool CanBuy (ShopItem item)
        {
            if (IsValid(item) == false)
                return false;
            return true;
        }

        public override void Buy (ShopItem item)
        {
            if (IsValid(item) == false)
                return;
            ShopInventoryItem iItem = item as ShopInventoryItem;
            _inventory.AddItem(iItem.Item.GetCopy());
        }
    }
}