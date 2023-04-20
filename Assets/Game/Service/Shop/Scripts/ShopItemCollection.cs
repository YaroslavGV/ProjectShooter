using System.Collections.Generic;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(fileName = "ItemCollection", menuName = "Shop/Item/Collection")]
    public class ShopItemCollection : ScriptableObject
    {
        [SerializeField] private ShopItem[] _items;

        public IEnumerable<ShopItem> Items => _items;
    }
}