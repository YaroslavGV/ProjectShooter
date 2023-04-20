using UnityEngine;
using StatSystem;
using InventorySystem;

namespace Unit
{
    using Skin;
    using Zenject;

    [CreateAssetMenu(fileName = "PlayerProfile", menuName = "Unit/PlayerProfileProvider")]
    public class PlayerProfileProvider : ScriptableObject
    {
        [SerializeField] UnitProfile _profile;
        [SerializeField] private string _name;
        [SerializeField] private StatValues _baseStats;
        private Loadout _loadout;
        private PlayerSkins _skins;

        public UnitProfile Profile => new UnitProfile
        {
            name = _name,
            baseStats = _baseStats,
            skin = _skins.SelectedSkin,
            equipment = _loadout.Equipment
        };

        [Inject]
        public void SetPlayerProperty (Loadout loadout, PlayerSkins skins)
        {
            _loadout = loadout;
            _skins = skins;
        }
    }
}