
namespace UnityEngine.UI
{
    public enum FadeAction { None, FadeIn, FadeOut }

    public abstract class Fade : MonoBehaviour
    {
        [SerializeField] private AnimationTransition _in = new AnimationTransition(0.5f);
        [SerializeField] private AnimationTransition _out = new AnimationTransition(0.5f);
        [Space]
        [SerializeField] private float _inValue = 1;
        [SerializeField] private float _outValue = 0;
        [Space]
        [SerializeField] private FadeAction _defaultState;
        [SerializeField] private FadeAction _onStart;

        public float InDuration => _in.duration;
        public float OutDuration => _out.duration;

        protected virtual void Start ()
        {
            ApplyDefaultState(_defaultState);
            ExecuteStartAction(_onStart);
        }

        public void DoFadeAction (FadeAction action)
        {
            if (action == FadeAction.FadeIn)
                FadeIn();
            else if (action == FadeAction.FadeIn)
                FadeOut();
        }

        public void FadeIn () => StartFadeAction(_inValue, _in);

        public void FadeOut () => StartFadeAction(_outValue, _out);

        protected abstract void StartFadeAction (float value, AnimationTransition transition);

        protected abstract void SetValue (float value);

        private void ApplyDefaultState (FadeAction action)
        {
            if (action == FadeAction.FadeIn)
                SetValue(_inValue);
            else if (action == FadeAction.FadeOut)
                SetValue(_outValue);
        }

        private void ExecuteStartAction (FadeAction action)
        {
            if (action == FadeAction.FadeIn)
                FadeIn();
            else if (action == FadeAction.FadeOut)
                FadeOut();
        }
    }
}
