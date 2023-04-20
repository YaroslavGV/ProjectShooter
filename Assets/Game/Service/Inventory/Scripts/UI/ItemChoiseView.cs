using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Weapon;
using Zenject;

namespace InventorySystem
{
    public class ItemChoiseView : MonoBehaviour
    {
        public UnityEvent OnChoise;
        public UnityEvent OnSelect;
        [SerializeField] private InventoryItemView _slotTemplate;
        [SerializeField] private Transform _slotsNode;
        private Inventory _inventory;
        private List<InventoryItemView> _slots;
        private Action<Item> _callback;

        [Inject]
        public void SetInventory (Inventory inventory)
        {
            _inventory = inventory;
            _slots = new List<InventoryItemView>();
        }

        public void Choise (Predicate<Item> filter, Action<Item> callback)
        {
            _callback = callback;
            SpawnSlots(filter);
            OnChoise?.Invoke();
        }

        public void ChoiseEquipable (WeaponType type, Action<Item> callback)
        {
            Choise(i => i is WeaponItem ei && ei.Type == type, callback);
        }

        private void SpawnSlots (Predicate<Item> filter)
        {
            ClearSlots();
            List<Item> validItems = _inventory.Items.ToList().FindAll(filter);
            foreach (var item in validItems)
            {
                InventoryItemView newSlot = Instantiate(_slotTemplate, _slotsNode);
                newSlot.SetItem(item);
                newSlot.OnClick += Select;
                _slots.Add(newSlot);
            }
        }

        private void ClearSlots ()
        {
            foreach (InventoryItemView slot in _slots)
            {
                slot.OnClick -= Select;
                Destroy(slot.gameObject);
            }
            _slots.Clear();
        }

        private void Select (Item item)
        {
            _callback?.Invoke(item);
            OnSelect?.Invoke();
        }
    }
}