using UnityEngine;

public class Test : MonoBehaviour
{
	[SerializeField] private Color _target;

    private void Start ()
    {
		var x = GetComponent<CanvasRenderer>();
		x.SetColor(_target);
    }
}