using System;

[Serializable]
public struct SettingsValues
{
    public bool vibraion;
    public bool stickFixedPosition;
    public string language;

    public override string ToString ()
    {
        string[] rows = {
            "Vibraion: " + vibraion,
            "stickFixedPosition: " + stickFixedPosition,
            "Language: " + language,
        };
        return string.Join(Environment.NewLine, rows);
    }
}
