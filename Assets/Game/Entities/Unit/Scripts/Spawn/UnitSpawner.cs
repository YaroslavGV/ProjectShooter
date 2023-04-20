using UnityEngine;
using FractionSystem;

namespace Unit
{
    [RequireComponent(typeof(SpawnUnitLocation))]
    public class UnitSpawner : MonoBehaviour
    {
        [SerializeField] private UnitProfileProvider _profileProvider;
        [SerializeField] private Fraction _fraction;
        
        public UnitModel Spawn ()
        {
            SpawnUnitLocation location = GetComponent<SpawnUnitLocation>();
            UnitModel unit = location.SpawnUnit(_profileProvider.Profile, _fraction);
            return unit;
        }
    }
}