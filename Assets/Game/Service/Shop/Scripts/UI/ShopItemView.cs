using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Shop
{
    public class ShopItemView : MonoBehaviour
    {
        public Action<ShopItem> OnClick;
        [SerializeField] private Image _icon;
        [Space]
        [SerializeField] private Button _button;
        private UnityAction _click;
        private ShopItem _item;

        public ShopItem Item => _item;

        private void OnEnable ()
        {
            _click += Click;
            _button.onClick.AddListener(_click);
        }

        private void OnDisable ()
        {
            _button.onClick.RemoveListener(_click);
            _click -= Click;
        }

        public void SetItem (ShopItem item)
        {
            _button.interactable = true;
            _item = item;
            UpdateView();
        }

        public void Unavailable ()
        {
            _button.interactable = false;
        }

        private void UpdateView ()
        {
            _icon.sprite = _item.Icon;
        }

        private void Click ()
        {
            OnClick?.Invoke(_item);
        }
    }
}