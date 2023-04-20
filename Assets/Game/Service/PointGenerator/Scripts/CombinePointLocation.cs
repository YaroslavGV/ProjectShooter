using System.Collections.Generic;
using UnityEngine;

namespace PointGenerator
{
    public class CombinePointLocation : CombineLocation
    {
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private List<ZoneLocation> _exceptionZones;

        public void AddSpawnPoint (Transform point)
        {
            if (_spawnPoints.Contains(point) == false)
                _spawnPoints.Add(point);
        }

        public void RemoveSpawnPoint (Transform point) => _spawnPoints.Remove(point);

        public void AddExceptionsZone (ZoneLocation zone)
        {
            if (_exceptionZones.Contains(zone) == false)
                _exceptionZones.Add(zone);
        }

        public void RemoveExceptionsZone (ZoneLocation zone) => _exceptionZones.Remove(zone);

        protected override Vector3 GetRandomPoint () => _spawnPoints[GetIndex()].position;

        protected override bool ExceptionsContain (Vector3 point) => ZonesContainPoint(_exceptionZones, point);

        private int GetIndex() => Random.Range(0, _spawnPoints.Count);
    }
}