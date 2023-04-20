using System;
using UnityEngine;
using Zenject;
using Memento;

namespace InventorySystem
{
    public class LoadoutInstaller : MonoInstaller
    {
        [SerializeField] private string _saveKey = "Loadout";
        [Space]
        [SerializeField] private ItemsCollection _itemsDataBase;
        [SerializeField] private EquipmentKey _defaultItems;
        [Space]
        [SerializeField] private bool _log = true;

        public override void InstallBindings ()
        {
            if (string.IsNullOrEmpty(_saveKey))
                throw new Exception("SaveKey is null or empty");
            if (_itemsDataBase == null)
                throw new Exception("ItemsCollection is missing");

            Loadout loadout = new Loadout();
            Container.Bind<Loadout>().FromInstance(loadout).AsSingle();

            MementoLoadout mLoadout = new MementoLoadout(loadout, _itemsDataBase, _defaultItems.Equipment);
            new JsonPlayerPrefsHandler(_saveKey, mLoadout);

            if (_log)
            {
                string text = ObjectLog.GetText(loadout, _saveKey);
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