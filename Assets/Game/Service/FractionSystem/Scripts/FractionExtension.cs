using System.Linq;
using System.Collections.Generic;

namespace FractionSystem
{
    public static class FractionExtension
    {
        public static bool IsEquals (this IFraction fractionA, IFraction fractionB) =>
            fractionA.ID == fractionB.ID;

        public static IEnumerable<IFractionMember> GetMembersOfFraction (this IEnumerable<IFractionMember> members, IFraction target) =>
            members.Where(m => m.Fraction.IsEquals(target));
    }
}