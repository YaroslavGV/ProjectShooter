using System.Collections;
using UnityEngine;

namespace Unit
{
    partial class ObjectTrackerUI : MonoBehaviour
    {
        private RectTransform _rect;
        private Camera _camera;
        private RectTransform _parent;
        
        public Transform Target { get; private set; }
        public bool IsTraking => Target != null;

        private void Awake ()
        {
            _rect = transform as RectTransform;
            _camera = Camera.main;
        }

        public void SetTarget (Transform target)
        {
            Stop();

            Target = target;
            _parent = transform.parent as RectTransform;
            StartCoroutine(Tracking());
        }

        public void Stop ()
        {
            StopAllCoroutines();
            Target = null;
        }

        private IEnumerator Tracking ()
        {
            while (true)
            {
                UpdatePosition();
                yield return null;
            }
        }

        private void UpdatePosition ()
        {
            Vector3 screenPosition = _camera.WorldToScreenPoint(Target.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_parent, screenPosition, null, out Vector2 localPoint);
            _rect.anchoredPosition = localPoint;
        }
    }
} 