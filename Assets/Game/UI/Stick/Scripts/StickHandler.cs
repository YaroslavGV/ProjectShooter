using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace Stick
{
    public class StickHandler : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public bool FixedPosition = false;
        [Space]
        [InputControl(layout = "Vector2")]
        [SerializeField] private string _controlPath;
        [Space]
        [SerializeField] private RectTransform _ancore;
        [SerializeField] private RectTransform _disc;
        [SerializeField] private RectTransform _handle;
        private Vector2 _ancoreOriginPosition;
        private Vector2 _ancorePosition;
        private float _discRadius;
        private float _dragRadius;
        private bool _isDrag;

        public Vector2 Axis { get; private set; }
        public string ControlPathInternal => _controlPath;

        protected override string controlPathInternal 
        { 
            get => _controlPath; 
            set => _controlPath = value; 
        }

        protected override void OnEnable ()
        {
            base.OnEnable();
            if (_ancoreOriginPosition == Vector2.zero)
                _ancoreOriginPosition = _ancore.anchoredPosition;

            _discRadius = _disc.rect.width * 0.5f;
            _dragRadius = _discRadius - _handle.rect.width * 0.5f;
        }

        public void OnPointerDown (PointerEventData eventData)
        {
            if (FixedPosition)
            {
                _ancorePosition = _ancoreOriginPosition;
                Vector2 touch = GetLocalPosition(eventData);
                Vector2 touchDelta = touch - _ancorePosition;
                _isDrag = touchDelta.sqrMagnitude < _discRadius * _discRadius;
            } 
            else
            {
                _ancorePosition = GetLocalPosition(eventData);
                _ancore.anchoredPosition = _ancorePosition;
                _isDrag = true;
            }
            if (_isDrag)
            {
                UpdateAxis(eventData);
            }
        }

        public void OnPointerUp (PointerEventData eventData)
        {
            if (_isDrag)
            {
                _ancore.anchoredPosition = _ancoreOriginPosition;

                Axis = Vector2.zero;
                SendValueToControl(Axis);

                _isDrag = false;
            }
        }

        public void OnDrag (PointerEventData eventData)
        {
            if (_isDrag)
                UpdateAxis(eventData);
        }

        private void UpdateAxis (PointerEventData eventData)
        {
            Vector2 position = GetLocalPosition(eventData);
            Vector2 delta = position - _ancorePosition;
            delta = delta / _dragRadius;

            Axis = Vector2.ClampMagnitude(delta, 1);
            SendValueToControl(Axis);
        }

        private Vector2 GetLocalPosition (PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, eventData.position, eventData.pressEventCamera, out Vector2 local);
            return local;
        }
    }
}