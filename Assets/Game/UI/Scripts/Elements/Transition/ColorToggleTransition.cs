using DG.Tweening;

namespace UnityEngine.UI
{
    public class ColorToggleTransition : MonoBehaviour
    {
        [SerializeField] private AnimationTransition _onTransition = new AnimationTransition(0.5f);
        [SerializeField] private Color _onColor = Color.white;
        [Space]
        [SerializeField] private AnimationTransition _offTransition = new AnimationTransition(0.5f);
        [SerializeField] private Color _offColor = Color.black;
        [Space]
        [SerializeField] private Graphic _target;
        private Tween _process;

        public void On () => StartTransition(_onTransition, _onColor);

        public void Off () => StartTransition(_offTransition, _offColor);

        private void StartTransition (AnimationTransition transition, Color color)
        {
            _process?.Kill();
            _process = _target.DOColor(color, transition.duration).
                SetEase(transition.curve).
                SetUpdate(UpdateType.Normal, true).
                SetLink(gameObject);
        }
    }
}