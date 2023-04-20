using UnityEngine;

namespace Stick
{
    public class StickHandle : MonoBehaviour
    {
        [SerializeField] private RectTransform _disc;
        [SerializeField] private RectTransform _handle;
        private float _dragRadius;

        private void OnEnable ()
        {
            float discRadius = _disc.rect.width * 0.5f;
            _dragRadius = discRadius - _handle.rect.width * 0.5f;
        }

        public void SetAxis (Vector2 axis)
        {
            _handle.anchoredPosition = axis * _dragRadius;
        }

        public void SetNeitral ()
        {
            _handle.anchoredPosition = Vector2.zero;
        }
    }
}