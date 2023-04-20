using UnityEngine;
using UnityEngine.UI;

namespace Currency.UI
{
    public class CurrencyBar : MonoBehaviour
    {
        [SerializeField] private CurrencyData _currency;
        [Space]
        [SerializeField] private Image _icon;
        [SerializeField] private Graphic[] _colorTargets;
        
        private void Start () => UpdateView();

        private void UpdateView ()
        {
            if (_icon != null)
                _icon.sprite = _currency.Icon;
            foreach (Graphic target in _colorTargets)
                if (target != null)
                    target.color = _currency.Color;
        }
    }
}