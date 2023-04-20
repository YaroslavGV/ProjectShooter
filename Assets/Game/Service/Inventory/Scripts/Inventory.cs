using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class Inventory
    {
        public Action<Item> OnAdd;
        public Action<Item> OnRemove;
        private readonly List<Item> _items;

        public Inventory ()
        {
            _items = new List<Item>();
        }

        public override string ToString () => 
            string.Join(Environment.NewLine, _items.Select(i => string.Format("{0} {1}", i.ID, i.Name)));

        public IEnumerable<Item> Items => _items;

        public void AddItem (Item item)
        {
            if (_items.Contains(item))
            {
                Debug.LogWarning("Inventory already contain item");
                return;
            }
            _items.Add(item);
            OnAdd?.Invoke(item);
        }

        public void RemoveItem (Item item)
        {
            if (_items.Contains(item) == false)
            {
                Debug.LogWarning("Inventory not contain item");
                return;
            }
            _items.Remove(item);
            OnRemove?.Invoke(item);
        }
    }
}