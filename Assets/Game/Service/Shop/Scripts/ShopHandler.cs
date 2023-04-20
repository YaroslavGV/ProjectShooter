using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Unit.Skin;

namespace Shop
{
    public class ShopHandler
    {
        public readonly ShopItemHandler[] _handlers;
        public readonly PlayerSkins _playSkin;

        public ShopHandler (IEnumerable<ShopItemHandler> itemHandlers)
        {
            _handlers = itemHandlers.ToArray();
        }

        public bool AvailableForBuy (ShopItem item)
        {
            foreach (var handler in _handlers)
                if (handler.IsProcessed(item))
                    if (handler.CanBuy(item))
                        return true;

            return false;
        }

        public void Buy (ShopItem item)
        {
            foreach (var handler in _handlers)
                if (handler.IsProcessed(item))
                {
                    handler.Buy(item);
                    return;
                }
            throw new Exception("Unknown item type");
        }
    }
}