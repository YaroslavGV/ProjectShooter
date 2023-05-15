using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Currency;
using InventorySystem;
using Unit.Skin;
using Zenject;

namespace Shop
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField] private ShopItemCollection _itemCollection;
        [SerializeField] private BuyButtonView _buyButton;
        [Space]
        [SerializeField] private ShopItemView _itemTamplate;
        [SerializeField] private Transform _itemsParent;
        private Wallet _wallet;
        private ShopHandler _shop;
        private List<ShopItemView> _items;

        private void OnEnable ()
        {
            if (_items == null)
                _items = new List<ShopItemView>();
            UpdateItems();
            _buyButton.OnClick += TryBuy;
        }

        private void OnDisable ()
        {
            _buyButton.OnClick -= TryBuy;
        }

        [Inject]
        public void SetPlayerProperty (Wallet wallet, Inventory inventory, PlayerSkins playerSkins)
        {
            _wallet = wallet;
            InventoryItemHandler inventoryHandler = new InventoryItemHandler(inventory);
            PlayerSkinsHandler skinHandler = new PlayerSkinsHandler(playerSkins);
            ShopItemHandler[] handlers = { inventoryHandler, skinHandler };
            _shop = new ShopHandler(handlers);
        }

        private void UpdateItems ()
        {
            ClearItems();
            foreach (ShopItem item in _itemCollection.Items)
            {
                if (_shop.AvailableForBuy(item)) {
                    ShopItemView itemView = Instantiate(_itemTamplate, _itemsParent);
                    itemView.SetItem(item);
                    itemView.OnClick += OnSelect;
                    _items.Add(itemView);
                }
            }
            _buyButton.Disable();
        }

        private void ClearItems ()
        {
            foreach (ShopItemView item in _items)
            {
                item.OnClick -= OnSelect;
                Destroy(item.gameObject);
            }
            _items.Clear();
        }

        private void OnSelect (ShopItem item)
        {
            _buyButton.DisplayPrice(item);
        }

        private void TryBuy (ShopItem item)
        {
            if (_wallet.FundsIsEnough(item.Price) == false)
            {
                Debug.Log("Funds are not enough");
                return;
            }
            try
            {
                _shop.Buy(item);
                _wallet.SpendFunds(item.Price);

                if (_shop.AvailableForBuy(item) == false)
                {
                    ShopItemView itemView = GetViewOf(item);
                    itemView.Unavailable();
                    _buyButton.Disable();
                }
            }
            catch (ArgumentException e)
            {
                Debug.LogWarning(e.Message);
            }
        }

        private ShopItemView GetViewOf (ShopItem item) => _items.FirstOrDefault(i => i.Item == item);
    }
}