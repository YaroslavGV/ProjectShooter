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
        [Space]
        [SerializeField] private string _missItemPrice = "-";
        [SerializeField] private Color _missItemColor = Color.white;
        private Wallet _wallet;
        private UnityAction _click;
        private ShopItem _item;

        public Button Button => _button;

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

        public void Disable ()
        {
            _item = null;
            UpdatePriceView(new CurrecnyValue());
            UpdatePurchaseAailability();
        }

        public void DisplayPrice (ShopItem item)
        {
            _item = item;
            UpdatePriceView(_item.Price);
            UpdatePurchaseAailability();
        }

        private void UpdatePriceView (CurrecnyValue price)
        {
            bool hasPrice = price.currency != null;
            CurrencyData currency = hasPrice ? price.currency : null;
            if (_priceAmount != null)
                _priceAmount.text = hasPrice ? currency.GetFormatValue(price.value) : _missItemPrice;
            if (_currencyIcon != null)
            {
                _currencyIcon.enabled = hasPrice;
                if (hasPrice)
                    _currencyIcon.sprite = currency.Icon;
            }
            Color color = hasPrice ? currency.Color : _missItemColor;
            foreach (var target in _colorTargets)
                target.color = color;
        }

        private void UpdatePurchaseAailability ()
        {
            _button.interactable = _item != null ? _wallet.FundsIsEnough(_item.Price) : false;
        }

        private void Click ()
        {
            OnClick?.Invoke(_item);
        }
    }
}