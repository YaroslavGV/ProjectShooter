using UnityEngine;
using Unit;

namespace Session
{
    [RequireComponent(typeof(GameSession))]
    public class SessionUnitHandler : MonoBehaviour
    {
        [SerializeField] private SceneUnits _units;
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private EnemySpawner _enemySpawner;
        private GameSession _session;
        private UnitModel _player;

        private void Awake ()
        {
            _session = GetComponent<GameSession>();
            _session.OnBagan += OnSessionBegan;

            _player = _playerSpawner.Spawn();
            _player.OnDead += OnPlayerDead;

            _units.OnRemoved += OnUnitRemove;
        }

        private void OnDestroy ()
        {
            _session.OnBagan -= OnSessionBegan;
            _units.OnRemoved -= OnUnitRemove;
        }

        private void OnSessionBegan ()
        {
            _enemySpawner.StartSpawn();
        }

        private void OnPlayerDead ()
        {
            _session.End();
            _enemySpawner.StopSpawn();
        }

        private void OnUnitRemove (UnitModel unit)
        {
            if (_player.CanAttack(unit))
                _session.Score.Earn(unit.Profile.score);
        }
    }
}