using UnityEngine;

namespace Weapon
{
    public class RayCastBullet : CastBullet
    {
        protected override RaycastHit[] GetHits (float deltaDistance) => Physics.RaycastAll(ForwardRay, deltaDistance, Mask);
        protected override float GetTravaledDistance (Vector3 point, RaycastHit hit) => Vector3.Distance(point, hit.point);
    }
}