using DG.Tweening;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioVolumeFade : Fade
    {
        private AudioSource _target;
        private Tween _process;

        protected override void Start ()
        {
            _target = GetComponent<AudioSource>();
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
            _target.volume = value;
        }
    }
}
