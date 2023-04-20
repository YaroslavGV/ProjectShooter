using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class TrailRicochetPoint : MonoBehaviour
{
    private TrailRenderer _trail;

    private void Awake ()
    {
        _trail = GetComponent<TrailRenderer>();
    }

    public void AddCurrentPosition ()
    {
        _trail.AddPosition(transform.position);
    }
}
