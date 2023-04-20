using DG.Tweening;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(Image))]
    public class ImageAlphaFade : Fade
    {
        private Image _target;
        private Tween _process;

        protected override void Start ()
        {
            _target = GetComponent<Image>();
            base.Start();
        }

        protected override void StartFadeAction (float value, AnimationTransition transition)
        {
            _process?.Kill();
            _process = _target.DOFade(value, transition.duration).
                SetEase(transition.curve).
                SetUpdate(UpdateType.Normal, true).
                SetLink(gameObject);
        }

        protected override void SetValue (float value)
        {
            Color color = _target.color;
            color.a = value;
            _target.color = color;
        }
    }
}
