using UnityEngine;
using StatSystem;
using InventorySystem;

namespace Unit
{
    using Skin;

    [CreateAssetMenu(fileName = "UnitProfile", menuName = "Unit/ProfileProvider")]
    public class UnitProfileProvider : ScriptableObject
    {
        [SerializeField] UnitProfile _profile;
        [SerializeField] private string _name;
        [SerializeField] private StatValues _baseStats;
        [SerializeField] private UnitSkin _skin;
        [SerializeField] private EquipmentKey _equipment;
        [SerializeField] private int _score;

        public UnitProfile Profile => new UnitProfile
        {
            name = _name,
            baseStats = _baseStats,
            skin = _skin,
            equipment = _equipment.Equipment,
            score = _score
        };
    }
}