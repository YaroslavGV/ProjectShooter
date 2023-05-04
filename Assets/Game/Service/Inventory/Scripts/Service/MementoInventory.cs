using System;
using System.Linq;
using UnityEngine;
using Memento;

namespace InventorySystem
{
    public class MementoInventory : Inventory, IJsonContent
    {
        [Serializable]
        private struct ItemIDs
        {
            public int[] elements;

            public ItemIDs (int[] elements) => this.elements = elements;
        }

        private readonly ItemsCollection _itemsDataBase;
        
        public MementoInventory (ItemsCollection itemsDataBase)
        {
            _itemsDataBase = itemsDataBase;
        
            OnAdd += OnContentChange;
            OnRemove += OnContentChange;
        }

        ~MementoInventory ()
        {
            OnAdd -= OnContentChange;
            OnRemove -= OnContentChange;
        }

        public Action ContentUpdated { get; set; }

        public string GetJson ()
        {
            int[] ids = items.Select(i => i.ID).ToArray();
            ItemIDs itemsID = new ItemIDs(ids);
            string json = JsonUtility.ToJson(itemsID);
            return json;
        }

        public void SetJson (string json)
        {
            ItemIDs itemsID = JsonUtility.FromJson<ItemIDs>(json);
            items.Clear();
            foreach (int id in itemsID.elements)
            {
                Item item = _itemsDataBase.GetItem(id);
                if (item != null)
                    items.Add(item);
            }
        }

        private void OnContentChange (Item item) => ContentUpdated?.Invoke();
    }
}