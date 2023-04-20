using System;
using UnityEngine;

namespace LogsView
{
    public class LogMassage
    {
        public Action OnIncrement;
        private readonly string _logString;
        private readonly string _stackTrace;
        private readonly LogType _type;
        private int _counter;

        public LogMassage (string logString, string stackTrace, LogType type)
        {
            _logString = logString;
            _stackTrace = stackTrace;
            _type = type;
            _counter = 1;
        }

        public string LogString => _logString;
        public string StackTrace => _stackTrace;
        public LogType Type => _type;
        public int Count => _counter;

        public bool SameContent (string logString, string stackTrace, LogType type) => _logString == logString && _stackTrace == stackTrace && _type == type;

        public void IncrementCount ()
        {
            _counter++;
            OnIncrement?.Invoke();
        }
    }
}