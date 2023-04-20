using UnityEngine;

namespace Currency
{
    [CreateAssetMenu(fileName = "CurrencyData", menuName = "Currency/Data")]
    public class CurrencyData : ScriptableObject
    {
        [SerializeField] private string _key = "Credit";
        [SerializeField] private Sprite _icon;
        [SerializeField] private Color _color = Color.white;

        public string Key => _key;
        public Sprite Icon => _icon;
        public Color Color => _color;

        public string GetFormatValue (int value) => value.ToString("N0").Replace(',', '.');
    }
}