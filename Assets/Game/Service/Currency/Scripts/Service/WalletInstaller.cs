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

            Wallet wallet = new Wallet();
            Container.Bind<Wallet>().FromInstance(wallet).AsSingle();

            MementoWallet mWallet = new MementoWallet(wallet, _default);
            new JsonPlayerPrefsHandler(_saveKey, mWallet);

            if (_log)
            {
                string text = ObjectLog.GetText(wallet, _saveKey);
                Debug.Log(text);
            }
        }

        [ContextMenu("ClearData")]
        private void ClearData ()
        {
            PlayerPrefs.DeleteKey(_saveKey);
            PlayerPrefs.Save();
        }
    }
}
