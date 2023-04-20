namespace UnityEngine.UI
{
    public class HoldCanvasGroupAgent : HoldAgent
    {
        [SerializeField] private float _normalAlpha = 0.5f;
        [SerializeField] private float _holdedAlpha = 1;
        [Space]
        [SerializeField] private CanvasGroup _target;

        public override void SetHoldingProgress (float value) => _target.alpha = Mathf.Lerp(_normalAlpha, _holdedAlpha, value);

        public override void SetReleasingProgress (float value) => _target.alpha = Mathf.Lerp(_holdedAlpha, _normalAlpha, value);
    }
}