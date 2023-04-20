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

        GameSettings settings = new GameSettings();
        Container.Bind<GameSettings>().FromInstance(settings).AsSingle();

        MementoSettings mSettings = new MementoSettings(settings, _default);
        new JsonPlayerPrefsHandler(_saveKey, mSettings);

        if (_log)
        {
            string text = ObjectLog.GetText(settings.Values, _saveKey);
            Debug.Log(text);
        }
    }

    [ContextMenu("ClearData")]
    private void ClearData ()
    {
        PlayerPrefs.DeleteKey(_saveKey);
        PlayerPrefs.Save();
    }
}