using System;

public class GameSettings
{
    public Action OnChange;
    protected SettingsValues values;

    public SettingsValues Values
    {
        get => values;
        set
        {
            values = value;
            OnChange?.Invoke();
        }
    }
}
