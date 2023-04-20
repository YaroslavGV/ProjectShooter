using UnityEngine;

namespace PointGenerator
{
    public class RectLocation : ZoneLocation
    {
        [SerializeField] private Vector2 _size = Vector2.one;

        public override float Square => _size.x * _size.y;

        public override Vector3 GetPoint ()
        {
            float x = Random.Range(-_size.x, _size.x) * 0.5f;
            float y = Random.Range(-_size.y, _size.y) * 0.5f;
            return transform.position + new Vector3(x, 0, y);
        }

        public override bool Contain (Vector3 position)
        {
            Vector2 origin = new Vector2(transform.position.x, transform.position.z);
            Vector2 target = new Vector2(position.x, position.z);
            Vector2 delta = target - origin;
            return Mathf.Abs(delta.x) < _size.x * 0.5f && Mathf.Abs(delta.y) < _size.y * 0.5f;
        }

        protected override void DrawGizmos () => GizmosUtility.DrawRectXZ(transform.position, _size);
    }
}