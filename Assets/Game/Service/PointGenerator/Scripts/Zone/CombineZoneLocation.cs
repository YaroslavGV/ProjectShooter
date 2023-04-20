using System.Collections.Generic;
using UnityEngine;

namespace PointGenerator
{
    public class CombineZoneLocation : CombineLocation
    {
        [SerializeField] private List<ZoneLocation> _spawnZones;
        [SerializeField] private List<ZoneLocation> _exceptionZones;

        public void AddSpawnZone (ZoneLocation zone)
        {
            if (_spawnZones.Contains(zone) == false)
                _spawnZones.Add(zone);
        }

        public void RemoveSpawnZone (ZoneLocation zone) => _spawnZones.Remove(zone);

        public void AddExceptionsZone (ZoneLocation zone)
        {
            if (_exceptionZones.Contains(zone) == false)
                _exceptionZones.Add(zone);
        }

        public void RemoveExceptionsZone (ZoneLocation zone) => _exceptionZones.Remove(zone);

        protected override Vector3 GetRandomPoint () 
        {
            int index = GetZoneIndexRelativeSquare();
            return _spawnZones[index].GetPoint();
        }

        private int GetZoneIndexRelativeSquare ()
        {
            int count = _spawnZones.Count;
            float totalSquare = 0;
            float[] squares = new float[count];
            for (int i = 0; i < count; i++)
            {
                squares[i] = _spawnZones[i].Square;
                totalSquare += squares[i];
            }

            float value = Random.Range(0, totalSquare);
            int targetIndex = 0;
            while (value > squares[targetIndex])
            {
                value -= squares[targetIndex];
                targetIndex++;
            }
            return targetIndex;
        }

        protected override bool ExceptionsContain (Vector3 point) => ZonesContainPoint(_exceptionZones, point);
    }
}