using System;
using UnityEngine;

namespace LogsView
{
    public partial class LogView
    {
        [Serializable]
        public struct LogTypeColors
        {
            public Color log;
            public Color warning;
            public Color error;
            public Color exception;
            public Color assert;

            public Color GetColor (LogType type)
            {
                switch (type)
                {
                    case LogType.Log: return log;
                    case LogType.Warning: return warning;
                    case LogType.Error: return error;
                    case LogType.Exception: return exception;
                    case LogType.Assert: return assert;
                    default: return Color.white;
                }
            }
        }
    }
}
