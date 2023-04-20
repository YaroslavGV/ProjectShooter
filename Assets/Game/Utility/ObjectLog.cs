using System;

public static class ObjectLog
{
    public static string GetText (object target, string header)
    {
        string contentString = target.ToString();
        string tabRows = "\t" + contentString.Replace("\n", Environment.NewLine + "\t");
        return header + Environment.NewLine + tabRows;
    }
}