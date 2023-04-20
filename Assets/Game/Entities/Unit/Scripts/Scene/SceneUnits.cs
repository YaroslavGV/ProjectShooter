using System;
using System.Collections.Generic;
using UnityEngine;
using FractionSystem;
using Zenject;

namespace Unit
{
    public class SceneUnits : MonoBehaviour
    {
        public Action<UnitModel> OnAdd;
        public Action<UnitModel> OnRemoved;
        [SerializeField] private UnitModel _unitTemplate;
        private List<UnitModel> _units = new List<UnitModel>();
        private int _counter;
        [Inject] private DiContainer _container;

        public IEnumerable<UnitModel> Units => _units;

        public void AddUnit (UnitModel unit)
        {
            if (_units.Contains(unit))
                return;
            _units.Add(unit);
            unit.OnDead += OnUnitDead;
            OnAdd?.Invoke(unit);
        }

        public void RemoveUnit (UnitModel unit)
        {
            if (_units.Remove(unit))
            {
                unit.OnDead -= OnUnitDead;
                OnRemoved?.Invoke(unit);
            }
        }

        public UnitModel FindEnemyFor (UnitModel unit)
        {
            foreach (UnitModel target in _units)
                if (unit.CanAttack(target))
                    return target;
            return null;
        }

        private void OnUnitDead ()
        {
            foreach (UnitModel unit in _units)
                if (unit.IsAlive == false)
                {
                    RemoveUnit(unit);
                    break;
                }
        }

        public UnitModel SpawnUnit (UnitProfile profile, Fraction fraction, Vector3 position, Quaternion rotation)
        {
            UnitModel unit = _container.InstantiatePrefabForComponent<UnitModel>(_unitTemplate, position, rotation, transform);
            unit.gameObject.name = string.Format("{0} {1}", profile.name, _counter.ToString("D3"));
            unit.Initialize(profile, fraction);
            AddUnit(unit);
            _counter++;
            return unit;
        }
    }
}