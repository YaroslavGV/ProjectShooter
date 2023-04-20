using System;

namespace StatSystem
{
    [Serializable]
    public class StatModifier
    {
        public StatType type;
        public float value;
        public ModifierSpecies species;

        public StatModifier (StatType type, float value, ModifierSpecies species = ModifierSpecies.Bonus) 
        {
            this.type = type;
            this.value = value;
            this.species = species;
        }

        public override string ToString() => $"{type} {value} {species}";
    }
}
