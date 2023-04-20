namespace UnityEngine.UI
{
    public class HoldScaleAgent : HoldAgent
    {
        [SerializeField] private Vector3 _normal = Vector3.one;
        [SerializeField] private Vector3 _holded = new Vector3(0.85f, 0.85f, 1);
        [Space]
        [SerializeField] private RectTransform _target;

        public override void SetHoldingProgress (float value) => _target.localScale = Vector3.Lerp(_normal, _holded, value);

        public override void SetReleasingProgress (float value) => _target.localScale = Vector3.Lerp(_holded, _normal, value);
    }
}