using UnityEngine.Events;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(Slider), typeof(AudioSource))]
    public class SliderTick : MonoBehaviour
    {
        [SerializeField] private float _tickEach = 5;
        [SerializeField] private float _minDelay = 0.1f;
        private Slider _slider;
        private AudioSource _audio;
        private UnityAction<float> _onChange;
        private int _currentTickValue;
        private float _backTimer;

        private void OnEnable ()
        {
            _slider = GetComponent<Slider>();
            _audio = GetComponent<AudioSource>();
            _onChange += OnChange;
            _slider.onValueChanged.AddListener(_onChange);
            _currentTickValue = GetTickValue();
        }

        private void OnDisable ()
        {
            _slider.onValueChanged.RemoveListener(_onChange);
            _onChange -= OnChange;
        }

        private void Update ()
        {
            if (_backTimer > 0)
            {
                _backTimer -= Time.unscaledDeltaTime;
                if (_backTimer < 0)
                    _backTimer = 0;
            }
        }

        private void OnChange (float value)
        {
            int newTickValue = GetTickValue();
            if (_currentTickValue != newTickValue)
            {
                _currentTickValue = newTickValue;
                if (_backTimer == 0)
                {
                    _audio.PlayOneShot(_audio.clip);
                    _backTimer = _minDelay;
                }
            }
        }

        private int GetTickValue () => Mathf.RoundToInt(_slider.value / _tickEach);
    }
}