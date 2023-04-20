using UnityEngine;

namespace PointGenerator
{
    public class SinglePointLocation : PointLocation
    {
        public override Vector3 GetPoint () => transform.position;

        protected override void DrawGizmos () => Gizmos.DrawSphere(transform.position, 0.1f);
    }
}