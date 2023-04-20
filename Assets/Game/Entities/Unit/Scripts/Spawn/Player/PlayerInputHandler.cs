using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Unit
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private InputActionReference _moveAction;
        [SerializeField] private InputActionReference _directionAction;
        private UnitModel _player;
        private Transform _camera;
        private InputAction _move;
        private InputAction _direction;
        
        public void SetPlayer (UnitModel player)
        {
            _player = player;

            _camera = Camera.main.transform;
            _move = _moveAction.ToInputAction();
            _direction = _directionAction.ToInputAction();
            StartCoroutine(UpdateInput());
        }

        private IEnumerator UpdateInput ()
        {
            while (_player.IsAlive)
            {
                InputValues values = _player.Inputs;
                Vector2 camera = CameraVector();

                Vector2 move = _move.ReadValue<Vector2>();
                if (move != Vector2.zero)
                    move = GetRelativeVector(move, camera);
                values.move = move;

                Vector2 direction = _direction.ReadValue<Vector2>();
                if (direction != Vector2.zero)
                    direction = GetRelativeVector(direction, camera);
                values.direction = direction;

                _player.Inputs = values;

                yield return null;
            }
        }

        private Vector2 CameraVector ()
        {
            Vector3 camera = _camera.right;
            return new Vector2(camera.x, camera.z).normalized;
        }

        private Vector2 GetRelativeVector (Vector2 target, Vector2 normalAxis)
        {
            return new Vector2(target.x * normalAxis.x - target.y * normalAxis.y, target.x * normalAxis.y + target.y * normalAxis.x);
        }
    }
}