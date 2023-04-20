using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DestroyParticleSystemStop : MonoBehaviour
{
    private void Start ()
    {
        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    private void OnParticleSystemStopped ()
    {
        Destroy(gameObject);
    }
}