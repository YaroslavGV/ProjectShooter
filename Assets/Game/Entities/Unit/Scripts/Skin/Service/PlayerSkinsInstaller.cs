using System;
using UnityEngine;
using Memento;
using Zenject;

namespace Unit.Skin
{
    public class PlayerSkinsInstaller : MonoInstaller
    {
        [SerializeField] private string _saveKey = "PlayerSkins";
        [Space]
        [SerializeField] private SkinCollection _skins;
        [Tooltip("Set first skin from collection if reference is missing")]
        [SerializeField] private UnitSkin _defaultSkin;
        [Space]
        [SerializeField] private bool _log = true;

        public override void InstallBindings ()
        {
            if (string.IsNullOrEmpty(_saveKey))
                throw new Exception("SaveKey is null or empty");
            if (_skins == null)
                throw new Exception("Skins is null");

            _skins.CheckKeyValid();
            string defaultSkin = _defaultSkin != null ? _defaultSkin.Key : "";
            MementoPlayerSkins skins = new MementoPlayerSkins(_skins.Elements, defaultSkin);
            Container.Bind<PlayerSkins>().FromInstance(skins).AsSingle();
            new JsonPlayerPrefsHandler(_saveKey, skins);

            if (_log)
            {
                string text = ObjectLog.GetText(skins, _saveKey);
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