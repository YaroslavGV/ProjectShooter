using UnityEngine;

namespace PointGenerator
{
    public class RingLocation : PointLocation
    {
        [SerializeField] private float _radius = 1;

        public override Vector3 GetPoint () 
        {
            float radian = Random.Range(0, Mathf.PI * 2f);
            Vector2 circlePoint = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * _radius;
            return new Vector3(circlePoint.x, transform.position.y, circlePoint.y);
        }

        protected override void DrawGizmos () => GizmosUtility.DrawRingXZ(transform.position, _radius);
    }
}