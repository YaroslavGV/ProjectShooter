using UnityEngine.Events;
using TMPro;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class SliderTextValue : MonoBehaviour
    {
        [SerializeField] private float _minValue = 0;
        [SerializeField] private float _maxValue = 100;
        [SerializeField] private string _prefix;
        [SerializeField] private string _suffix;
        [Space]
        [SerializeField] private Slider _slider;
        private TextMeshProUGUI _text;
        private UnityAction<float> _onChange;

        private void OnEnable ()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _onChange += OnChange;
            _slider.onValueChanged.AddListener(_onChange);
            OnChange(_slider.value);
        }

        private void OnDisable ()
        {
            _slider.onValueChanged.RemoveListener(_onChange);
            _onChange -= OnChange;
        }

        private void OnChange (float value)
        {
            float normalValue = Mathf.InverseLerp(_slider.minValue, _slider.maxValue, value);
            float rangeValue = Mathf.Lerp(_minValue, _maxValue, normalValue);
            _text.text = _prefix + rangeValue + _suffix;
        }
    }
}