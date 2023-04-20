using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventorySystem
{
    public class LoadoutView : MonoBehaviour
    {
        [SerializeField] LoadoutSlotView[] _slots;
        [Space]
        [SerializeField] private ItemChoiseView _ItemChoise;
        private Inventory _inventory;
        private Loadout _loadout;
        private LoadoutSlotView _targetSlot;

        private void OnEnable ()
        {
            foreach (LoadoutSlotView lSlot in _slots)
            {
                Item item = _loadout.GetItem(lSlot.Slot);
                lSlot.SetItem(item);
                lSlot.OnClick += OnClick;
            }
        }

        private void OnDisable ()
        {
            foreach (LoadoutSlotView lSlot in _slots)
                lSlot.OnClick -= OnClick;
        }

        [Inject]
        public void SetPlayerItems (Inventory inventory, Loadout loadout)
        {
            _inventory = inventory;
            _loadout = loadout;
        }

        private void OnClick (LoadoutSlotView slot)
        {
            _targetSlot = slot;
            _ItemChoise.ChoiseEquipable(_targetSlot.Type, OnSelect);
        }

        public void OnSelect (Item item)
        {
            _targetSlot.SetItem(item);

            _inventory.RemoveItem(item);
            Item removedItem = _loadout.Equipe(_targetSlot.Slot, item);
            if (removedItem != null)
                _inventory.AddItem(removedItem);
        }
    }
}