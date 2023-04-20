using UnityEngine.Events;

namespace UnityEngine.UI
{
    public class MenuSwitchAgent : MonoBehaviour
    {
        [SerializeField] private Key _key;
        [Space]
        public UnityEvent OnShow;
        public UnityEvent OnHide;

        public void Show (Key targetKey)
        {
            if (targetKey == _key)
                OnShow?.Invoke();
        }

        public void Hide (Key exceptionKey)
        {
            if (exceptionKey != _key)
                OnHide?.Invoke();
        }
    }
}