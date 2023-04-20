using System;
using System.Linq;
using System.Collections.Generic;

namespace Currency
{
    public class Wallet
    {
        public Action OnChange;
        private readonly Dictionary<string, int> _currencys;

        public Wallet ()
        {
            _currencys = new Dictionary<string, int>();
        }

        public override string ToString () => 
            string.Join(Environment.NewLine, _currencys.Select(c => string.Format("{0}: {1}", c.Key, c.Value)));

        public int GetFunds (string currencyKey)
        {
            if (_currencys.ContainsKey(currencyKey))
                return _currencys[currencyKey];
            return 0;
        }

        public void AddFunds (string currencyKey, int amount)
        {
            if (amount < 0)
                throw new Exception("Can't add negative amount");
            if (amount > 0)
                SetFunds(currencyKey, GetFunds(currencyKey) + amount);
        }

        public void SpendFunds (string currencyKey, int amount)
        {
            if (amount < 0)
                throw new Exception("Can't spend negative amount");
            if (GetFunds(currencyKey) < amount)
                throw new Exception("Insufficient funds (" + currencyKey + ")");
            SetFunds(currencyKey, GetFunds(currencyKey) - amount);
        }

        public void Clear ()
        {
            if (_currencys.Count > 0)
            {
                _currencys.Clear();
                OnChange?.Invoke();
            }
        }

        private void SetFunds (string currencyKey, int value)
        {
            if (_currencys.ContainsKey(currencyKey))
                _currencys[currencyKey] = value;
            else
                _currencys.Add(currencyKey, value);
            OnChange?.Invoke();
        }

        public bool ContainsCurrency (string currency) => _currencys.ContainsKey(currency);

        public string[] GetCurrencys () => _currencys.Select(c => c.Key).ToArray();
    }
}