using UnityEngine.Events;

namespace UnityEngine.Localization.Settings
{
    public class LanguageChangeEvent : MonoBehaviour
    {
        [SerializeField] private Locale _taret;
        [SerializeField] private LocalizationSettings _settings;
        [Space]
        public UnityEvent OnTarget;
        public UnityEvent OnMissTarget;

        private void OnEnable ()
        {
            OnChange(_settings.GetSelectedLocale());
            _settings.OnSelectedLocaleChanged += OnChange;
        }

        private void OnDisable () => _settings.OnSelectedLocaleChanged -= OnChange;

        private void OnChange (Locale locale)
        {
            if (locale == _taret)
                OnTarget?.Invoke();
            else
                OnMissTarget?.Invoke();
        }
    }
}