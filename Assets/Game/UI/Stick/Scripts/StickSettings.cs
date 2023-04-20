using UnityEngine;
using Zenject;

namespace Stick
{
    public class StickSettings : MonoBehaviour
    {
        [SerializeField] private StickHandler[] _sticks;
        private GameSettings _settings;

        private void Start ()
        {
            UpdateSettings();
            _settings.OnChange += OnChange;
        }

        private void OnDestroy ()
        {
            _settings.OnChange -= OnChange;
        }

        [Inject]
        public void SetSetings (GameSettings settings)
        {
            _settings = settings;
        }

        private void OnChange ()
        {
            UpdateSettings();
        }

        private void UpdateSettings ()
        {
            foreach (StickHandler stick in _sticks)
                stick.FixedPosition = _settings.Values.stickFixedPosition;
        }
    }
}