using UnityEngine;

namespace StatSystem
{
    public class ModifierEditable : PropertyAttribute
    {
        public bool type;
        public bool species;

        public ModifierEditable (bool type, bool species)
        {
            this.type = type;
            this.species = species;
        }
    }
}
