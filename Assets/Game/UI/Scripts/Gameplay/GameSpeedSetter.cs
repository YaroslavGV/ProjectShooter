using UnityEngine;

public class GameSpeedSetter : MonoBehaviour
{
    [SerializeField] private bool _setOnStart = true;
    [SerializeField] private float _speed = 1;

    public void Start ()
    {
        if (_setOnStart)
            SetSpeed(_speed);
    }

    public void SetSpeed (float speed) => Time.timeScale = speed;
}
