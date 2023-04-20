using UnityEngine;
using FractionSystem;

namespace Unit
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerProfileProvider _profile;
        [SerializeField] private Fraction _fraction;
        [Space]
        [SerializeField] private PlayerInputHandler _playerInput;
        [SerializeField] private UnitTracker _cameraTemplate;
        
        public UnitModel Spawn ()
        {
            SpawnUnitLocation location = GetComponent<SpawnUnitLocation>();
            UnitModel player = location.SpawnUnit(_profile.Profile, _fraction);
            _playerInput.SetPlayer(player);
            if (_cameraTemplate != null)
            {
                Transform parent = transform;
                if (transform.parent != null)
                    parent = transform.parent;
                UnitTracker tracker = Instantiate(_cameraTemplate, parent);
                tracker.SetTarget(player);
            }
            return player;
        }
    }
}