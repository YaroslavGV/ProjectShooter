using UnityEngine.Events;
using Zenject;

namespace UnityEngine.UI
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private Toggle _vibration;
        [SerializeField] private Toggle _stick;
        private GameSettings _gameSettings;
        private UnityAction<bool> _change;

        public void OnEnable ()
        {
            _change += Save;
            if (_vibration != null) 
            {
                _vibration.isOn = _gameSettings.Values.vibraion;
                _vibration.onValueChanged.AddListener(_change);
            }
            if (_stick != null)
            {
                _stick.isOn = _gameSettings.Values.stickFixedPosition;
                _stick.onValueChanged.AddListener(_change);
            }
        }

        private void OnDisable ()
        {
            if (_vibration != null)
                _vibration.onValueChanged.RemoveListener(_change);
            if (_stick != null)
                _stick.onValueChanged.RemoveListener(_change);
            _change -= Save;
        }

        [Inject]
        public void SetSettings (GameSettings settings)
        {
            _gameSettings = settings;
        }

        private void Save (bool value)
        {
            SettingsValues values = _gameSettings.Values;
            if (_vibration != null)
                values.vibraion = _vibration.isOn;
            if (_stick != null)
                values.stickFixedPosition = _stick.isOn;
            _gameSettings.Values = values;
        }
    }
}