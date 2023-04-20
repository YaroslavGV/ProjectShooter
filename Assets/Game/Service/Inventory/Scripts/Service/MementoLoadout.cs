using System;
using UnityEngine;
using Memento;

namespace InventorySystem
{
    public class MementoLoadout : IJsonContent
    {
        private readonly ItemsCollection _itemsDataBase;
        private readonly Loadout _loadout;
        private readonly Equipment _defaultItems;

        public Action ContentUpdated { get; set; }

        public MementoLoadout (Loadout loadout, ItemsCollection itemsDataBase, Equipment defaultItems)
        {
            _loadout = loadout;
            _itemsDataBase = itemsDataBase;
            _defaultItems = defaultItems;

            _loadout.OnChange += OnChange;
        }

        ~MementoLoadout () => _loadout.OnChange -= OnChange;

        public string GetJson ()
        {
            EquipmentID equipe = _loadout.Equipment.EquipmentID;
            string json = JsonUtility.ToJson(equipe);
            return json;
        }

        public void SetJson (string json)
        {
            EquipmentID equipe = JsonUtility.FromJson<EquipmentID>(json);
            foreach (SlotItemID slotItem in equipe.items)
            {
                Item item = _itemsDataBase.GetItem(slotItem.itemID);
                if (item != null)
                    _loadout.Equipe(slotItem.slot, item);
            }
        }

        public void SetDefault ()
        {
            foreach (SlotItem slotItem in _defaultItems.items)
                _loadout.Equipe(slotItem);
        }

        private void OnChange () => ContentUpdated?.Invoke();
    }
}