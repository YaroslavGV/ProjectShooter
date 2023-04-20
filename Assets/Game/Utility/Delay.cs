using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Delay : MonoBehaviour
{
    [SerializeField] private float _delay = 1;
    [SerializeField] private bool _realtime = true;
    [Space]
    [SerializeField] private bool _beganOnStart;
    [Space]
    public UnityEvent Action;

    private void Start ()
    {
        if (_beganOnStart)
            Begin();
    }

    public void Begin ()
    {
        StopAllCoroutines();
        StartCoroutine(DalayeProcess());
    }

    private IEnumerator DalayeProcess ()
    {
        if (_realtime)
            yield return new WaitForSecondsRealtime(_delay);
        else
            yield return new WaitForSeconds(_delay);
        Action?.Invoke();
    }
}
