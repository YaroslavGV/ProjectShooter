using System.Collections.Generic;
using UnityEngine;

namespace PointGenerator
{
    public abstract class CombineLocation : PointLocation
    {
        private const int tryCount = 100;

        public override Vector3 GetPoint ()
        {
            int tryNumber = 0;
            Vector3 point;
            do
            {
                point = GetRandomPoint();
                tryNumber++;
            }
            while (tryNumber < tryCount && ExceptionsContain(point));
            if (tryNumber == tryCount)
                Debug.LogWarning("Can't find point out of exception zones");
            return point;
        }

        protected abstract Vector3 GetRandomPoint ();

        protected abstract bool ExceptionsContain (Vector3 point);

        protected bool ZonesContainPoint (IEnumerable<ZoneLocation> zones, Vector3 point)
        {
            foreach (ZoneLocation zone in zones)
                if (zone.Contain(point))
                    return true;
            return false;
        }
    }
}