using System;
using System.Collections;
using System.Collections.Generic;
using Zenject;

namespace UnityEngine.Localization.Settings
{
    /// <summary> Save locale when changed and restores at startup </summary>
    public class CurrentLocaleKeeper : MonoBehaviour
    {
        private static CurrentLocaleKeeper _instance;
        [SerializeField] private LocalizationSettings _settings;
        private GameSettings _gameSettings;

        private IEnumerator Start ()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                yield return LocalizationSettings.InitializationOperation;
                Restores();
                _settings.OnSelectedLocaleChanged += Save;
            }
        }

        private void OnDestroy () => _settings.OnSelectedLocaleChanged -= Save;

        [Inject]

        public void SetSettings (GameSettings settings)
        {
            _gameSettings = settings;
        }

        private void Save (Locale locale)
        {
            SettingsValues values = _gameSettings.Values;
            values.language = GetLocaleKey(locale);
            _gameSettings.Values = values;
        }

        private void Restores ()
        {
            try
            {
                string localeName = _gameSettings.Values.language;
                if (string.IsNullOrEmpty(localeName) == false)
                {
                    Locale locale = GetLocale(localeName);
                    _settings.SetSelectedLocale(locale);
                }
            }
            catch (ArgumentException e)
            {
                Debug.LogError(e.Message);
            }
        }

        private string GetLocaleKey (Locale locale) => locale.Identifier.Code;

        private Locale GetLocale (string localeKey)
        {
            List<Locale> locales = _settings.GetAvailableLocales().Locales;
            if (locales.Count == 0)
                throw new Exception("Locales is empty");
            foreach (Locale locale in locales)
                if (GetLocaleKey(locale) == localeKey)
                    return locale;
            throw new Exception("Cant find locale with key "+ localeKey);
        }
    }
}