
namespace UnityEngine.UI
{
    public class ToggleImage : MonoBehaviour
    {
        [SerializeField] private Image _target;
        [Space]
        [SerializeField] private Sprite _on;
        [SerializeField] private Sprite _off;
        
        public void SetOn (bool on) => _target.sprite = on ? _on : _off;
    }
}