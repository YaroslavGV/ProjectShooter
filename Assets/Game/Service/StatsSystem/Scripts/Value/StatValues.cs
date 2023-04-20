using System;

namespace StatSystem
{
    [Serializable]
    public struct StatValues
    {
        public StatValue[] values;

        public override string ToString () => string.Join(Environment.NewLine, values);
    }
}
