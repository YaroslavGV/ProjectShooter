using System;
using UnityEngine;
using UnityEngine.UI;

namespace Unit
{
    [RequireComponent(typeof(ObjectTrackerUI))]
    public class UnitUI : MonoBehaviour, IConsumable<UnitUI>
    {
        [SerializeField] private RectTransform _line;
        [SerializeField] private GameObject _view;
        [Space]
        [SerializeField] private Graphic _colorTarget;
        [SerializeField] private Gradient _colorGradient = GradientConstructor.GetGradient(Color.white, Color.gray);
        [Space]
        [SerializeField] private Key _taretPoint;
        private Action<UnitUI> _consumeCallback;
        private ObjectTrackerUI _tracker;
        private UnitModel _targetunit;
        private bool _isVisible;
        
        private void OnDestroy ()
        {
            Stop();
        }

        public void SetConsumeCallback (Action<UnitUI> callback) => _consumeCallback = callback;

        public void SetUnit (UnitModel unit) 
        {
            Stop();

            _targetunit = unit;
            _targetunit.Health.OnChange += OnHealthChange;
            _targetunit.OnDead += OnTargetDead;
            OnHealthChange();

            _tracker = GetComponent<ObjectTrackerUI>();
        }

        public void Stop ()
        {
            StopAllCoroutines();
            if (_targetunit != null)
            {
                _targetunit.Health.OnChange -= OnHealthChange;
                _targetunit.OnDead -= OnTargetDead;
            }
            
            SetVisible(false);
        }

        private void OnTargetDead ()
        {
            Stop();
            _consumeCallback?.Invoke(this);
        }

        private void OnHealthChange ()
        {
            float health = _targetunit.Health.RelativeValue;
            bool isVisible = health < 1;

            if (_isVisible != isVisible)
                SetVisible(isVisible);
            if (isVisible)
                UpdateView(health);
        }

        private void UpdateView (float relativeHealth)
        {
            _colorTarget.color = _colorGradient.Evaluate(1 - relativeHealth);
            _line.anchorMax = new Vector2(relativeHealth, 1);
        }

        private void SetVisible (bool visible)
        {
            _isVisible = visible;
            _view.SetActive(_isVisible);
            if (_tracker != null)
            {
                if (_isVisible)
                    _tracker.SetTarget(GetTrackTarget(_targetunit));
                else
                    _tracker.Stop();
            }
        }

        private Transform GetTrackTarget (UnitModel unit)
        {
            Transform target = unit.Skin.GetPoint(_taretPoint.Name);
            if (target == null)
                return unit.transform;
            return target;
        }
    }
} 