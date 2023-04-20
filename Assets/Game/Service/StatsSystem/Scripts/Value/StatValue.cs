using System;

namespace StatSystem
{
    [Serializable]
    public struct StatValue 
    {
        public StatType type;
        public float value;

        public StatValue (StatType type, float value) 
        {
            this.type = type;
            this.value = value;
        }

        public override string ToString () => string.Format("{0}: {1}", type, value);
    }
}