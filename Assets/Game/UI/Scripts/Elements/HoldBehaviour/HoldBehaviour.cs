using DG.Tweening;

namespace UnityEngine.UI
{

    public class HoldBehaviour : MonoBehaviour
    {
        [SerializeField] private float _holdDuration = 0.15f;
        [SerializeField] private float _releaseDuration = 0.15f;
        [SerializeField] private bool _brackTransition = false;
        [Space]
        [SerializeField] private HoldAgent[] _agents;
        private Tween _tween;
        private bool _isHold;
        private bool _onAction;

        public void HoldDown ()
        {
            if (_isHold)
                return;

            _isHold = true;
            if (_onAction == false || _brackTransition)
                DoHold();
        }

        public void HoldUp ()
        {
            if (_isHold == false)
                return;

            _isHold = false;
            if (_onAction == false || _brackTransition)
                DoRelease();
        }

        private void OnHoldComplete ()
        {
            _onAction = false;
            if (_isHold == false)
                DoRelease();
        }

        private void OnReleaseComplete ()
        {
            _onAction = false;
            if (_isHold)
                DoHold();
        }

        private void SetHoldingProgress (float value)
        {
            foreach (HoldAgent agent in _agents)
                agent.SetHoldingProgress(value);
        }

        private void SetReleasingProgress (float value)
        {
            foreach (HoldAgent agent in _agents)
                agent.SetReleasingProgress(value);
        }

        private void DoHold ()
        {
            _onAction = true;
            StopTransition();
            _tween = DOTween.To(v => SetHoldingProgress(v), 0, 1, _holdDuration).
                    SetUpdate(UpdateType.Normal, true).
                    SetLink(gameObject).
                    OnComplete(OnHoldComplete);
        }
        private void DoRelease ()
        {
            _onAction = true;
            StopTransition();
            _tween = DOTween.To(v => SetReleasingProgress(v), 0, 1, _releaseDuration).
                    SetUpdate(UpdateType.Normal, true).
                    SetLink(gameObject).
                    OnComplete(OnReleaseComplete);
        }

        private void StopTransition ()
        {
            if (_tween != null)
            {
                _tween.Kill();
                _tween = null;
            }
        }
    }
}