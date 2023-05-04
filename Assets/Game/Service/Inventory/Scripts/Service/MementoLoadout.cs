using System;
using UnityEngine;
using Memento;

namespace InventorySystem
{
    public class MementoLoadout : Loadout, IJsonContent
    {
        private readonly ItemsCollection _itemsDataBase;
        
        public MementoLoadout (ItemsCollection itemsDataBase)
        {
            _itemsDataBase = itemsDataBase;
            OnChange += OnContentChange;
        }

        ~MementoLoadout () => OnChange -= OnContentChange;

        public Action ContentUpdated { get; set; }

        public string GetJson () => JsonUtility.ToJson(Equipment.EquipmentID);

        public void SetJson (string json)
        {
            EquipmentID equipe = JsonUtility.FromJson<EquipmentID>(json);
            items.Clear();
            foreach (SlotItemID slotItem in equipe.items)
            {
                Item item = _itemsDataBase.GetItem(slotItem.itemID);
                if (item != null)
                    items.Add(new SlotItem(slotItem.slot, item));
            }
        }

        private void OnContentChange () => ContentUpdated?.Invoke();
    }
}