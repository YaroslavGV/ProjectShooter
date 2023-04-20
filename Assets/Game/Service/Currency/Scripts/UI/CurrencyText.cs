using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;
using DG.Tweening;

namespace Currency.UI
{
    public class CurrencyText : MonoBehaviour
    {
        [SerializeField] private CurrencyData _currency;
        [SerializeField] private AnimationTransition _transition = new AnimationTransition(0.1f);
        [Space]
        [SerializeField] private TextMeshProUGUI _amount;
        private Wallet _wallet;
        private int _currentAmount;
        private Tween _process;

        public CurrencyData Currency => _currency;

        private void OnDestroy ()
        {
            if (_wallet != null)
                _wallet.OnChange -= OnChange;
        }

        [Inject]
        public void SetWallet (Wallet wallet)
        {
            _wallet = wallet;
            _wallet.OnChange += OnChange;
            SetAmount(_wallet.GetFunds(_currency.Key));
        }

        private void OnChange ()
        {
            if (_wallet.GetFunds(_currency.Key) != _currentAmount)
                UpdateValue();
        }

        private void UpdateValue ()
        {
            int target = _wallet.GetFunds(_currency.Key);
            if (_transition.duration <= 0)
                SetAmount(target);
            else
            {
                _process?.Kill();
                _process = DOTween.To(v => SetAmount((int)v), _currentAmount, target, _transition.duration).
                    SetEase(_transition.curve).
                    SetUpdate(UpdateType.Normal, true).
                    SetLink(gameObject);
            }
        }

        private void SetAmount (int amount)
        {
            _currentAmount = amount;
            _amount.text = _currency.GetFormatValue(_currentAmount);
        }
    }
}