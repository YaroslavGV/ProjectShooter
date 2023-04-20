using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Currency
{
    [CreateAssetMenu(fileName = "CurrencyDataCollection", menuName = "Currency/DataCollection")]
    public class CurrencyDataCollection : ScriptableObject
    {
        [SerializeField] private CurrencyData[] _currencys;

        public IEnumerable<CurrencyData> Currencys => _currencys;

        public CurrencyData GetCurrency (string key) => _currencys.First(c => c.Key == key);
    }
}