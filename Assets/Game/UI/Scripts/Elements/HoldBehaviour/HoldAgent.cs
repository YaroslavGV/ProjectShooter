namespace UnityEngine.UI
{
    public abstract class HoldAgent : MonoBehaviour
    {
        public abstract void SetHoldingProgress (float value);

        public abstract void SetReleasingProgress (float value);
    }
}