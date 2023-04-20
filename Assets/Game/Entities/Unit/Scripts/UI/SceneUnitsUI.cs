using UnityEngine;

namespace Unit
{
    public class UnitUIPool : ObjectPool<UnitUI>
    {
    }

    public class SceneUnitsUI : MonoBehaviour
    {
        [SerializeField] private UnitUI _template;
        [SerializeField] private SceneUnits _units;
        [Space]
        [SerializeField] private PoolSettings _pool;
        private UnitUIPool _uis;
        
        public void Awake ()
        {
            _uis = gameObject.AddComponent<UnitUIPool>();
            _uis.Initialize(_pool, _template);
            _units.OnAdd += OnAdd;
        }

        private void OnDestroy()
        {
            if (_units != null)
                _units.OnAdd -= OnAdd;
        }

        private void OnAdd (UnitModel unit)
        {
            var hud = _uis.Pool.Get();
            hud.SetUnit(unit);
        }
    }
}