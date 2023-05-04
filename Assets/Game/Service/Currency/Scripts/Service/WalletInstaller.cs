using System;
using UnityEngine;
using Zenject;
using Memento;

namespace Currency
{
    public class WalletInstaller : MonoInstaller
    {
        [SerializeField] private string _saveKey = "Wallet";
        [Space]
        [SerializeField] private CurrecnyValue[] _default;
        [Space]
        [SerializeField] private bool _log = true;
        
        public override void InstallBindings ()
        {
            if (string.IsNullOrEmpty(_saveKey))
                throw new Exception("SaveKey is null or empty");

            MementoWallet wallet = new MementoWallet();
            Container.Bind<Wallet>().FromInstance(wallet).AsSingle();
            new JsonPlayerPrefsHandler(_saveKey, wallet, GetDefaultJson);

            if (_log)
            {
                string text = ObjectLog.GetText(wallet, _saveKey);
                Debug.Log(text);
            }
        }

        private string GetDefaultJson ()
        {
            MementoWallet defaultWallet = new MementoWallet();
            foreach (CurrecnyValue cv in _default)
                defaultWallet.AddFunds(cv.currency.Key, cv.value);
            return defaultWallet.GetJson();
        }

        [ContextMenu("ClearData")]
        private void ClearData ()
        {
            PlayerPrefs.DeleteKey(_saveKey);
            PlayerPrefs.Save();
        }
    }
}
