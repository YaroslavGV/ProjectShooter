namespace UnityEngine.Localization.Settings
{
    public class LanguageChange : MonoBehaviour
    {
        [SerializeField] private Locale _target;
        [SerializeField] private LocalizationSettings _settings;

        public void Change () => _settings.SetSelectedLocale(_target);
    }
}