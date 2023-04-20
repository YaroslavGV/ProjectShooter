using UnityEngine;
using Zenject;

namespace Unit.Skin
{
    public class CurrentSkinSetter : MonoBehaviour
    {
        [SerializeField] private SkinSpawner _skinSpawner;
        private PlayerSkins _playerSkins;

        private void OnEnable ()
        {
            UpdateSelectedSkin();
        }

        [Inject]
        public void SetPlayerSkins (PlayerSkins playerSkins)
        {
            _playerSkins = playerSkins;
        }

        private void UpdateSelectedSkin ()
        {
            if (_skinSpawner != null)
                _skinSpawner.SpawnSkin(_playerSkins.SelectedSkin);
        }
    }
}