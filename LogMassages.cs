using System;
using System.Collections.Generic;
using UnityEngine;

public class LogMassages
{
    private const int _capacity = 64;
    public Action<LogMassage> OnAdd;
    public Action<LogMassage> OnRemove;
    public Action OnClear;
    private readonly List<LogMassage> _massages;

    public IEnumerable<LogMassage> Massages => _massages;

    public LogMassages (int capacity = 32)
    {
        if (capacity > 0)
            _massages = new List<LogMassage>(capacity + 1);
        else
            _massages = new List<LogMassage>();
        Application.logMessageReceived += AddLog;
    }

    public void AddLog (string logString, string stackTrace, LogType type)
    {
        if (HaveSameLog(logString, stackTrace, type))
            return;

        LogMassage massage = new LogMassage(logString, stackTrace, type);
        _massages.Add(massage);
        OnAdd?.Invoke(massage);
        if (_capacity > 0 && _massages.Count > _capacity)
        {
            LogMassage rMassage = _massages[0];
            _massages.RemoveAt(0);
            OnRemove?.Invoke(rMassage);
        }
    }

    public void ClearLogs ()
    {
        _massages.Clear();
        OnClear?.Invoke();
    }

    private bool HaveSameLog (string logString, string stackTrace, LogType type)
    {
        foreach (LogMassage massage in _massages)
            if (massage.SameContent(logString, stackTrace, type))
            {
                massage.IncrementCount();
                return true;
            }
        return false;
    }
}