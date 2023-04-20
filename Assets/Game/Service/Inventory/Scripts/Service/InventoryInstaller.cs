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

            Inventory inventory = new Inventory();
            Container.Bind<Inventory>().FromInstance(inventory).AsSingle();

            MementoInventory mInventory = new MementoInventory(inventory, _itemsDataBase, _defaultItems);
            new JsonPlayerPrefsHandler(_saveKey, mInventory);

            if (_log)
            {
                string text = ObjectLog.GetText(inventory, _saveKey);
                Debug.Log(text);
            }
        }

        [ContextMenu("ClearData")]
        private void ClearData ()
        {
            PlayerPrefs.DeleteKey(_saveKey);
            PlayerPrefs.Save();
        }
    }
}