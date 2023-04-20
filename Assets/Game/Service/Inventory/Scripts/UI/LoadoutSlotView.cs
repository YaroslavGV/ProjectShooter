using System;
using UnityEngine;
using UnityEngine.UI;
using Weapon;

namespace InventorySystem
{
    public class LoadoutSlotView : MonoBehaviour
    {
        public Action<LoadoutSlotView> OnClick;
        [SerializeField] private Key _slot;
        [SerializeField] private WeaponType _type;
        [Space]
        [SerializeField] private Image _icon;
        private Item _item;

        public string Slot => _slot.Name;
        public WeaponType Type => _type;

        public void Click ()
        {
            OnClick?.Invoke(this);
        }

        public void SetItem (Item item)
        {
            _item = item;
            UpdateView();
        }

        private void UpdateView ()
        {
            _icon.enabled = _item != null;
            if (_item != null)
                _icon.sprite = _item.Icon;
        }
    }
}