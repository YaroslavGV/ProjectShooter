using System;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class InventoryItemView : MonoBehaviour
    {
        public Action<Item> OnClick;
        [SerializeField] private Image _icon;
        private Item _item;

        public void SetItem (Item item)
        {
            _item = item;
            _icon.sprite = _item.Icon;
        }

        public void Click ()
        {
            OnClick?.Invoke(_item);
        }
    }
}