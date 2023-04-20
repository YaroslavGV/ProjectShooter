using System;
using UnityEngine;
using UnityEngine.Localization.Settings;
using Memento;

public class MementoSettings : IJsonContent
{
    private readonly GameSettings _settings;
    private readonly SettingsValues _default;

    public MementoSettings (GameSettings settings, SettingsValues defaultValues)
    {
        _settings = settings;
        _default = defaultValues;

        _settings.OnChange += OnChange;
    }

    ~MementoSettings () => _settings.OnChange -= OnChange;

    public Action ContentUpdated { get; set; }

    public string GetJson () => JsonUtility.ToJson(_settings.Values);

    public void SetJson (string json)
    {
        SettingsValues values = JsonUtility.FromJson<SettingsValues>(json);
        _settings.Values = values;
    }

    public void SetDefault () 
    { 
        _settings.Values = _default;
    }

    private void OnChange () => ContentUpdated?.Invoke();
}
