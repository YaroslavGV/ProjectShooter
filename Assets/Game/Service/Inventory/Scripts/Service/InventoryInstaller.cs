using System;
using UnityEngine;
using Zenject;
using Memento;

namespace InventorySystem
{
    public class InventoryInstaller : MonoInstaller
    {
        [SerializeField] private string _saveKey = "Inventory";
        [Space]
        [SerializeField] private ItemsCollection _itemsDataBase;
        [SerializeField] private Item[] _defaultItems;
        [Space]
        [SerializeField] private bool _log = true;

        public override void InstallBindings ()
        {
            if (string.IsNullOrEmpty(_saveKey))
                throw new Exception("SaveKey is null or empty");
            if (_itemsDataBase == null)
                throw new Exception("ItemsCollection is missing");
            _itemsDataBase.CheckIDCollision();

            MementoInventory inventory = new MementoInventory(_itemsDataBase);
            Container.Bind<Inventory>().FromInstance(inventory).AsSingle();
            new JsonPlayerPrefsHandler(_saveKey, inventory, GetDefaultJson);

            if (_log)
            {
                string text = ObjectLog.GetText(inventory, _saveKey);
                Debug.Log(text);
            }
        }

        private string GetDefaultJson ()
        {
            MementoInventory defaultInventory = new MementoInventory(_itemsDataBase);
            foreach (Item item in _defaultItems)
                defaultInventory.AddItem(item);
            return defaultInventory.GetJson();
        } 

        [ContextMenu("ClearData")]
        private void ClearData ()
        {
            PlayerPrefs.DeleteKey(_saveKey);
            PlayerPrefs.Save();
        }
    }
}