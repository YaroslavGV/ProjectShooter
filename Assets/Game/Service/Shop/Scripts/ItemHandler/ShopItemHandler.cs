using System;
using UnityEngine;

namespace Shop
{
    public abstract class ShopItemHandler
    {
        public abstract Type ProcessedType { get; }

        public abstract bool CanBuy (ShopItem item);
        public abstract void Buy (ShopItem item);

        public bool IsProcessed (ShopItem item) => item.GetType().Equals(ProcessedType);

        protected bool IsValid (ShopItem item)
        {
            bool valid = IsProcessed(item);
            if (valid == false)
                Debug.LogWarning("Wrong item type. Require " + ProcessedType);
            return valid;
        }
    }
}