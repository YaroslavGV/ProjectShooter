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

            MementoLoadout loadout = new MementoLoadout(_itemsDataBase);
            Container.Bind<Loadout>().FromInstance(loadout).AsSingle();
            new JsonPlayerPrefsHandler(_saveKey, loadout, GetDefaultJson);

            if (_log)
            {
                string text = ObjectLog.GetText(loadout, _saveKey);
                Debug.Log(text);
            }
        }

        private string GetDefaultJson ()
        {
            MementoLoadout defaultLoadout = new MementoLoadout(_itemsDataBase);
            foreach (SlotKeyItem slotItem in _defaultItems.items)
                defaultLoadout.Equipe(slotItem.PointItem);
            return defaultLoadout.GetJson();
        }

        [ContextMenu("ClearData")]
        private void ClearData ()
        {
            PlayerPrefs.DeleteKey(_saveKey);
            PlayerPrefs.Save();
        }
    }
}