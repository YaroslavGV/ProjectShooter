namespace StatSystem
{
    public class OwnedStatModifire : StatModifier
    {
        public object sorce;

        public OwnedStatModifire (StatType type, float value, ModifierSpecies species, object sorce) :
            base(type, value, species)
        {
            this.sorce = sorce;
        }

        public OwnedStatModifire (StatModifier modifire, object sorce) : this(modifire.type, modifire.value, modifire.species, sorce) { }

        public override string ToString () => $"{base.ToString()} {sorce}";
    }
}
