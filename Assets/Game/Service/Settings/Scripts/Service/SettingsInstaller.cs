using System;
using UnityEngine;
using Memento;
using Zenject;

public class SettingsInstaller : MonoInstaller
{
    [SerializeField] private string _saveKey = "Settings";
    [SerializeField] private SettingsValues _default;
    [Space]
    [SerializeField] private bool _log = true;

    public override void InstallBindings ()
    {
        if (string.IsNullOrEmpty(_saveKey))
            throw new Exception("SaveKey is null or empty");

        MementoSettings settings = new MementoSettings();
        Container.Bind<GameSettings>().FromInstance(settings).AsSingle();
        new JsonPlayerPrefsHandler(_saveKey, settings, GetDefaultJson);

        if (_log)
        {
            string text = ObjectLog.GetText(settings.Values, _saveKey);
            Debug.Log(text);
        }
    }

    private string GetDefaultJson ()
    {
        MementoSettings defaultSettings = new MementoSettings();
        defaultSettings.Values = _default;
        return defaultSettings.GetJson();
    }

    [ContextMenu("ClearData")]
    private void ClearData ()
    {
        PlayerPrefs.DeleteKey(_saveKey);
        PlayerPrefs.Save();
    }
}