using System;
using UnityEngine;
using Memento;

public class MementoSettings : GameSettings, IJsonContent
{
    public MementoSettings () => OnChange += OnContentChange;

    ~MementoSettings () => OnChange -= OnContentChange;

    public Action ContentUpdated { get; set; }

    public string GetJson () => JsonUtility.ToJson(values);

    public void SetJson (string json) => values = JsonUtility.FromJson<SettingsValues>(json);

    private void OnContentChange () => ContentUpdated?.Invoke();
}
