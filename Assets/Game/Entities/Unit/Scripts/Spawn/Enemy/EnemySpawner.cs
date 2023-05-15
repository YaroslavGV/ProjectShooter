using System.Collections;
using UnityEngine;
using FractionSystem;

namespace Unit
{
    [RequireComponent(typeof(SpawnUnitLocation))]
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private UnitProfileProvider _profileProvider;
        [SerializeField] private Fraction _fraction;
        [Space]
        [SerializeField] private float _spawnDelay = 3;
        [Space]
        [SerializeField] private BotInputHandler _botInput;
        private SpawnUnitLocation _location;
        private IEnumerator _process;

        public void StartSpawn ()
        {
            if (_process != null)
                return;
            
            _location = GetComponent<SpawnUnitLocation>();
            _process = SpawnProcess();
            StartCoroutine(_process);
        }

        public void StopSpawn ()
        {
            if (_process == null)
                return;
            StopCoroutine(_process);
            _process = null;
        }

        private IEnumerator SpawnProcess ()
        {
            while (true)
            {
                Spawn();
                yield return new WaitForSeconds(_spawnDelay);
            }
        }

        private void Spawn ()
        {
            UnitModel unit = _location.SpawnUnit(_profileProvider.Profile, _fraction);
            _botInput.AddUnit(unit);
        }
    }
}