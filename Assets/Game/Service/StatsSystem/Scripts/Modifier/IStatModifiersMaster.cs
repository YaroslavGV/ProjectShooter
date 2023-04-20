using System.Collections.Generic;

namespace StatSystem
{
    public interface IStatModifiersMaster
    {
        IEnumerable<StatModifier> GetStatModifiers ();
    }
}
