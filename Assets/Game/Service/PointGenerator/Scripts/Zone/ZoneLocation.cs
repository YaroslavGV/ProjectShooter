using UnityEngine;

namespace PointGenerator
{
    public abstract class ZoneLocation : PointLocation
    {
        public abstract float Square { get; }

        public abstract bool Contain (Vector3 position);
    }
}