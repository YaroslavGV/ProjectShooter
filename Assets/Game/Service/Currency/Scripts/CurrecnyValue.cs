using System;

namespace Currency
{
    [Serializable]
    public struct CurrecnyValue
    {
        public CurrencyData currency;
        public int value;
    }
}