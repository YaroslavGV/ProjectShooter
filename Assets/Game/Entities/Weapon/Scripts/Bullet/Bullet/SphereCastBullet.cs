using UnityEngine;

namespace Weapon
{
    public class SphereCastBullet : CastBullet
    {
        [SerializeField] private float _radius = 0.2f;

        protected override RaycastHit[] GetHits (float deltaDistance) => Physics.SphereCastAll(ForwardRay, _radius, deltaDistance, Mask);
        protected override float GetTravaledDistance (Vector3 point, RaycastHit hit)
        {
            Vector3 hitSpherePoint = hit.point+hit.normal*_radius;
            return Vector3.Distance(point, hitSpherePoint);
        }
    }
}