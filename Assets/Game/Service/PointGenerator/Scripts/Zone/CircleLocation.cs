using UnityEngine;

namespace PointGenerator
{
    public class CircleLocation : ZoneLocation
    {
        [SerializeField] private float _radius = 1;

        public override float Square => _radius * _radius * Mathf.PI;

        public override Vector3 GetPoint ()
        {
            Vector2 random = Random.insideUnitCircle * _radius;
            return transform.position + new Vector3(random.x, 0, random.y);
        }

        public override bool Contain (Vector3 position)
        {
            Vector2 origin = new Vector2(transform.position.x, transform.position.z);
            Vector2 target = new Vector2(position.x, position.z);
            Vector2 delta = target - origin;
            return delta.sqrMagnitude < _radius * _radius;
        }

        protected override void DrawGizmos () => GizmosUtility.DrawRingXZ(transform.position, _radius);
    }
}