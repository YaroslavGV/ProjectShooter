using System;

public class GameSettings
{
    public Action OnChange;
    private SettingsValues _values;

    public SettingsValues Values
    {
        get => _values;
        set
        {
            _values = value;
            OnChange?.Invoke();
        }
    }
}
