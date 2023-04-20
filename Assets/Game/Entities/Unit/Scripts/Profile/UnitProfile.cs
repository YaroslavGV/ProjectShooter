using StatSystem;
using InventorySystem;

namespace Unit
{
    using Skin;

    public struct UnitProfile
    {
        public string name;
        public StatValues baseStats;
        public UnitSkin skin;
        public Equipment equipment;
        public int score;
    }
}