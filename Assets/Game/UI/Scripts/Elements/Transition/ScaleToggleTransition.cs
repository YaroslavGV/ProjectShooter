using DG.Tweening;

namespace UnityEngine.UI
{
    public class ScaleToggleTransition : MonoBehaviour
    {
        [SerializeField] private AnimationTransition _onTransition = new AnimationTransition(0.5f);
        [SerializeField] private Vector3 _onScale = new Vector3(1.25f, 1.25f, 1);
        [Space]
        [SerializeField] private AnimationTransition _offTransition = new AnimationTransition(0.5f);
        [SerializeField] private Vector3 _offScale = Vector3.one;
        [Space]
        [SerializeField] private RectTransform _target;
        private Tween _process;

        public void On () => StartTransition(_onTransition, _onScale);

        public void Off () => StartTransition(_offTransition, _offScale);

        private void StartTransition (AnimationTransition transition, Vector3 scale)
        {
            _process?.Kill();
            _process = _target.DOScale(scale, transition.duration).
                SetEase(transition.curve).
                SetUpdate(UpdateType.Normal, true).
                SetLink(gameObject);
        }
    }
}