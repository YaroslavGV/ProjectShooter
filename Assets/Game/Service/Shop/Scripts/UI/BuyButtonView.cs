using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Currency;
using Zenject;

namespace Shop
{
    public class BuyButtonView : MonoBehaviour
    {
        public Action<ShopItem> OnClick;
        [SerializeField] private TextMeshProUGUI _priceAmount;
        [SerializeField] private Image _currencyIcon;
        [SerializeField] private Graphic[] _colorTargets;
        [Space]
        [SerializeField] private Button _button;
        private Wallet _wallet;
        private UnityAction _click;
        private ShopItem _item;

        private void OnEnable ()
        {
            _click += Click;
            _button.onClick.AddListener(_click);
            _wallet.OnChange += UpdatePurchaseAailability;
        }

        private void OnDisable ()
        {
            _button.onClick.RemoveListener(_click);
            _click -= Click;
            _wallet.OnChange -= UpdatePurchaseAailability;
        }

        [Inject]
        public void SetWallet (Wallet wallet)
        {
            _wallet = wallet;
        }

        public void DisplayPrice (ShopItem item)
        {
            _item = item;
            UpdateView(_item.Price);
            UpdatePurchaseAailability();
        }

        private void UpdateView (CurrecnyValue price)
        {
            CurrencyData currency = price.currency;
            if (_priceAmount != null)
                _priceAmount.text = currency.GetFormatValue(price.value);
            if (_currencyIcon != null)
                _currencyIcon.sprite = currency.Icon;
            foreach (var target in _colorTargets)
                target.color = currency.Color;
        }

        private void UpdatePurchaseAailability ()
        {
            _button.interactable = _wallet.FundsIsEnough(_item.Price);
        }

        private void Click ()
        {
            OnClick?.Invoke(_item);
        }
    }
}