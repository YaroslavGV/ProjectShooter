using UnityEngine;

namespace FractionSystem
{
    [CreateAssetMenu(fileName = "Fraction", menuName = "Fraction/Fraction")]
    public class Fraction : ScriptableObject, IFraction
    {
        public int ID => GetInstanceID();
        public string Name => name;
    }
}