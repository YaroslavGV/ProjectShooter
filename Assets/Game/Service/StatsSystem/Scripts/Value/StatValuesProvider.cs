using UnityEngine;

namespace StatSystem
{
    [CreateAssetMenu(fileName = "StatValues", menuName = "Stat/ValuesProvider")]
    public class StatValuesProvider : ScriptableObject
    {
        [SerializeField] StatValues _stats;

        public StatValues Stats => _stats;
    }
}
