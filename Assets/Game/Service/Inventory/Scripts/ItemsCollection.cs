using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "ItemsCollection", menuName = "Inventory/ItemsCollection")]
    public class ItemsCollection : ScriptableObject
    {
        [SerializeField] private Item[] _items;

        public IEnumerable<Item> Items => _items;

        public Item GetItem (int id, bool instance = false)
        {
            foreach (Item item in _items)
                if (item.ID == id)
                {
                    if (instance)
                        return item;
                    else
                        return Instantiate(item);
                }
            Debug.LogWarning("Can`t find item with ID: " + id);
            return null;
        }

        public void CheckIDCollision ()
        {
            for (int i = 0; i < _items.Length; i++)
                for (int j = i + 1; j < _items.Length; j++)
                    if (_items[i].ID == _items[j].ID)
                        Debug.LogWarning(string.Format("Item ID \"{0}\" collision on index {1} and {2}", _items[i].ID, i, j));
        }
    }
}