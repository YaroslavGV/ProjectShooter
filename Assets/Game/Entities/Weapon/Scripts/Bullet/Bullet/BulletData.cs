using UnityEngine;

namespace Weapon
{
    public struct ShotData
    {
        public Transform point;
        public Vector3 vector;

        public ShotData (Transform point, Vector3 vector)
        {
            this.point = point;
            this.vector = vector;
        }

        public ShotData (Transform point) : this(point, point.forward)
        {
        }
    }

    public struct HitData
    {
        public Collider collider;
        public Vector3 collisionPoint;
        public Vector3 surfaceNormal;
        public bool isFinished;

        public HitData (Collider collider, Vector3 collisionPoint, Vector3 surfaceNormal ,bool isFinished)
        {
            this.collider = collider;
            this.collisionPoint = collisionPoint;
            this.surfaceNormal = surfaceNormal;
            this.isFinished = isFinished;
        }
    }

    public struct ExhaustData
    {
        public Vector3 position;
        public Vector3 vector;

        public ExhaustData (Vector3 position, Vector3 vector)
        {
            this.position = position;
            this.vector = vector;
        }
    }
}