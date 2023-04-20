namespace UnityEngine.UI
{
    public class HoldColorAgent : HoldAgent
    {
        [SerializeField] private Color _normal = Color.white;
        [SerializeField] private Color _holded = Color.gray;
        [Space]
        [SerializeField] private Graphic _target;

        public override void SetHoldingProgress (float value) => _target.color = Color.Lerp(_normal, _holded, value);

        public override void SetReleasingProgress (float value) => _target.color = Color.Lerp(_holded, _normal, value);
    }
}