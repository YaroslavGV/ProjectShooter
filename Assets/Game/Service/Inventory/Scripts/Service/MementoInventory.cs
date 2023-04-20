using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Memento;

namespace InventorySystem
{
    public class MementoInventory : IJsonContent
    {
        [Serializable]
        private struct ItemIDs
        {
            public int[] elements;

            public ItemIDs (int[] elements) => this.elements = elements;
        }

        private readonly ItemsCollection _itemsDataBase;
        private readonly Inventory _inventory;
        private readonly IEnumerable<Item> _defaultItems;

        public Action ContentUpdated { get; set; }

        public MementoInventory (Inventory inventory, ItemsCollection itemsDataBase, IEnumerable<Item> defaultItems)
        {
            _inventory = inventory;
            _itemsDataBase = itemsDataBase;
            _defaultItems = defaultItems;

            _inventory.OnAdd += OnChange;
            _inventory.OnRemove += OnChange;
        }

        ~MementoInventory ()
        {
            _inventory.OnAdd -= OnChange;
            _inventory.OnRemove -= OnChange;
        }

        public string GetJson ()
        {
            int[] ids = _inventory.Items.Select(i => i.ID).ToArray();
            ItemIDs itemsID = new ItemIDs(ids);
            string json = JsonUtility.ToJson(itemsID);
            return json;
        }

        public void SetJson (string json)
        {
            ItemIDs itemsID = JsonUtility.FromJson<ItemIDs>(json);
            foreach (int id in itemsID.elements)
            {
                Item item = _itemsDataBase.GetItem(id);
                if (item != null)
                    _inventory.AddItem(item);
            }
        }

        public void SetDefault ()
        {
            foreach (Item item in _defaultItems)
                _inventory.AddItem(item.GetCopy());
        }

        private void OnChange (Item item) => ContentUpdated?.Invoke();
    }
}