using System.Collections.Generic;
using UnityEngine;

namespace FractionSystem
{
    [CreateAssetMenu(fileName = "FractionCollection", menuName = "Fraction/Collection")]
    public class FractionCollection : ScriptableObject
    {
        [SerializeField] private Fraction[] _collection;

        public IEnumerable<IFraction> Collection => _collection;
    }
}