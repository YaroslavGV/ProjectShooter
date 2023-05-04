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
        protected readonly List<Item> items = new List<Item>();

        public override string ToString () => 
            string.Join(Environment.NewLine, items.Select(i => string.Format("{0} {1}", i.ID, i.Name)));

        public IEnumerable<Item> Items => items;

        public void AddItem (Item item)
        {
            if (items.Contains(item))
            {
                Debug.LogWarning("Inventory already contain item");
                return;
            }
            items.Add(item);
            OnAdd?.Invoke(item);
        }

        public void RemoveItem (Item item)
        {
            if (items.Contains(item) == false)
            {
                Debug.LogWarning("Inventory not contain item");
                return;
            }
            items.Remove(item);
            OnRemove?.Invoke(item);
        }
    }
}