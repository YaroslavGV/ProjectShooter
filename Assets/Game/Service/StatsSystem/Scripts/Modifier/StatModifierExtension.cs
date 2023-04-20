using System.Linq;
using System.Collections.Generic;

namespace StatSystem
{
    public static class StatModifierExtension
    {
        public static OwnedStatModifire GetOwned (this StatModifier modifire, object sorce) => new OwnedStatModifire(modifire, sorce);

        public static IEnumerable<OwnedStatModifire> GetOwned (this IEnumerable<StatModifier> modifires, object sorce) => modifires.Select(m => new OwnedStatModifire(m, sorce));
    }
}
