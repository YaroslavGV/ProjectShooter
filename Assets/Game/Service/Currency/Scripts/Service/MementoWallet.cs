using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Memento;

namespace Currency
{
    public class MementoWallet : IJsonContent
    {
        [Serializable]
        private struct KeyAmount
        {
            public string key;
            public int amount;

            public KeyAmount (KeyValuePair<string, int> pair)
            {
                key = pair.Key;
                amount = pair.Value;
            }
        }

        [Serializable]
        private struct KeyAmountCollection
        {
            public KeyAmount[] elements;

            public KeyAmountCollection (KeyValuePair<string, int>[] elements) =>
                this.elements = elements.Select(e => new KeyAmount(e)).ToArray();
        }

        private readonly Wallet _wallet;
        private readonly CurrecnyValue[] _default;

        public MementoWallet (Wallet wallet, CurrecnyValue[] defaultValues)
        {
            _wallet = wallet;
            _default = defaultValues;

            _wallet.OnChange += OnChange;
        }

        ~MementoWallet () => _wallet.OnChange -= OnChange;

        public Action ContentUpdated { get; set; }

        public string GetJson ()
        {
            string[] currencyKeys = _wallet.GetCurrencys();
            
            KeyValuePair<string, int>[] values = new KeyValuePair<string, int>[currencyKeys.Length];
            for (int i = 0; i < currencyKeys.Length; i++) 
            {
                string key = currencyKeys[i];
                int value = _wallet.GetFunds(currencyKeys[i]);
                values[i] = new KeyValuePair<string, int>(key, value);
            }

            KeyAmountCollection saveData = new KeyAmountCollection(values);
            return JsonUtility.ToJson(saveData);
        }

        public void SetJson (string json)
        {
            KeyAmountCollection saveData = JsonUtility.FromJson<KeyAmountCollection>(json);
            if (saveData.elements == null)
                return;
            foreach (KeyAmount element in saveData.elements)
                _wallet.AddFunds(element.key, element.amount);
        }

        public void SetDefault ()
        {
            _wallet.Clear();
            foreach (CurrecnyValue pair in _default)
                _wallet.AddFunds(pair.currency.Key, pair.value);
        }

        private void OnChange () => ContentUpdated?.Invoke();
    }
}