using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Unit.Skin
{
    public class PlayerSkinChoiseView : MonoBehaviour
    {
        [SerializeField] private SkinSpawner _skinSpawner;
        [Space]
        [Tooltip("Skin count")]
        public UnityEvent<int> OnInit;
        [Tooltip("Skin index")]
        public UnityEvent<int> OnSelect;
        [Space]
        public UnityEvent OnShowSelected;
        public UnityEvent OnShowAvailable;
        public UnityEvent OnShowUnavailable;

        private PlayerSkins _playerSkins;
        private UnitSkin[] _skins;
        private int _selectedIndex;

        private void OnEnable ()
        {
            OnInit?.Invoke(_skins.Length);
            UpdateSkin();
            InvokeSelected();
        }

        [Inject]
        public void SetPlayerSkins (PlayerSkins playerSkins)
        {
            _playerSkins = playerSkins;
            _skins = _playerSkins.Skins.ToArray();
            _selectedIndex = Array.IndexOf(_skins, _playerSkins.SelectedSkin);
        }

        /// <summary> automatic wrap index </summary>
        public void ShowSkinIndex (int index)
        {
            index = WrapIndex(index);

            if (_selectedIndex != index)
            {
                _selectedIndex = index;
                UpdateSkin();
                InvokeSelected();
            }
        }

        /// <summary> automatic wrap to first index </summary>
        public void SelectNextSkinIndex () => ShowSkinIndex(_selectedIndex + 1);

        /// <summary> automatic wrap to last index </summary>
        public void SelectPriviousSkinIndex () => ShowSkinIndex(_selectedIndex - 1);

        public void SelectCurrent ()
        {
            _playerSkins.SelectSkin(_skins[_selectedIndex]);
        }

        private void UpdateSkin ()
        {
            if (_skinSpawner != null)
                _skinSpawner.SpawnSkin(_skins[_selectedIndex]);
        }

        private void InvokeSelected ()
        {
            if (_skins[_selectedIndex] == _playerSkins.SelectedSkin)
                OnShowSelected?.Invoke();
            else if (_playerSkins.IsAvailable(_skins[_selectedIndex]))
                OnShowAvailable?.Invoke();
            else
                OnShowUnavailable?.Invoke();

            OnSelect?.Invoke(_selectedIndex);
        }

        private int WrapIndex (int index)
        {
            if (index > _skins.Length - 1)
                index %= _skins.Length;
            while (index < 0)
                index += _skins.Length;
            return index;
        }
    }
}