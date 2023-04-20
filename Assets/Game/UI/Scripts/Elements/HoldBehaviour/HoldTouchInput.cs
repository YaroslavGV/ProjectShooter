using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(HoldBehaviour))]
    public class HoldTouchInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private HoldBehaviour _behaviour;

        private void OnEnable () => _behaviour = GetComponent<HoldBehaviour>();

        public void OnPointerDown (PointerEventData pointerEventData) => _behaviour.HoldDown();

        public void OnPointerUp (PointerEventData pointerEventData) => _behaviour.HoldUp();
    }
}