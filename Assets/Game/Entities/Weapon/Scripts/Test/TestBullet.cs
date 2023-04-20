using System.Collections;
using UnityEngine;

namespace Weapon
{
    public abstract class TestBullet : MonoBehaviour
    {
        [SerializeField] private float _spwnDelay = 1;
        [SerializeField] private bool _spawnOnStart = true;
        [SerializeField] private bool _logOnSpawn = true;
        [Space]
        [SerializeField] private Transform _spawnPoint;
        private IEnumerator _spawnProgress;

        private Transform SpawnPoint => _spawnPoint != null ? _spawnPoint : transform;

        private void Start ()
        {
            OnStart();
            if (_spawnOnStart)
                StartSpawn();
        }

        protected virtual void OnStart () { }
        protected abstract void Shot (ShotData data);

        [ContextMenu("Start Spawn")]
        public void StartSpawn ()
        {
            if (_spawnProgress != null)
                return;
            _spawnProgress = SpawnProgress();
            StartCoroutine(_spawnProgress);
        }

        [ContextMenu("Stop Spawn")]
        public void StopSpawn ()
        {
            if (_spawnProgress == null)
                return;
            StopCoroutine(_spawnProgress);
            _spawnProgress = null;
        }

        public void SpawnBullet ()
        {
            Transform point = SpawnPoint;
            Shot(new ShotData(point));
            if (_logOnSpawn)
                Debug.Log("Bulled Spawned");
        }

        private IEnumerator SpawnProgress ()
        {
            while (true)
            {
                SpawnBullet();
                yield return new WaitForSeconds(_spwnDelay);
            }
        }
    }
}