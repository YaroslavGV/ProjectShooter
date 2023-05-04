using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Memento;

namespace Currency
{
    public class MementoWallet : Wallet, IJsonContent
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

            public KeyAmountCollection (IEnumerable<KeyValuePair<string, int>> elements) =>
                this.elements = elements.Select(e => new KeyAmount(e)).ToArray();
        }

        public MementoWallet () => OnChange += OnContentChange;

        ~MementoWallet () => OnChange -= OnContentChange;

        public Action ContentUpdated { get; set; }

        public string GetJson () => JsonUtility.ToJson(new KeyAmountCollection(currencys));

        public void SetJson (string json)
        {
            KeyAmountCollection saveData = JsonUtility.FromJson<KeyAmountCollection>(json);
            if (saveData.elements == null)
                return;
            currencys.Clear();
            foreach (KeyAmount element in saveData.elements)
                currencys.Add(element.key, element.amount);
        }

        private void OnContentChange () => ContentUpdated?.Invoke();
    }
}
