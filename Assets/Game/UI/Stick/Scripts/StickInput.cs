using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Stick
{
    [RequireComponent(typeof(StickHandler))]
    public abstract class StickInput : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _inputAsset;
        private InputAction _axis;

        public Vector2 Axis { get; private set; }

        protected virtual void OnEnable ()
        {
            StickHandler stick = GetComponent<StickHandler>();
            _axis = FindAction(stick.ControlPathInternal);
        }

        protected virtual void Update ()
        {
            Axis = _axis.ReadValue<Vector2>();
        }

        private InputAction FindAction (string path)
        {
            foreach (InputActionMap map in _inputAsset.actionMaps)
            {
                InputAction targetAction = FindAction(map.actions, path);
                if (targetAction != null)
                    return targetAction;
            }
            return null;
        }

        private InputAction FindAction (IEnumerable<InputAction> actions, string path)
        {
            foreach (InputAction action in actions)
                foreach (InputBinding bind in action.bindings)
                    if (bind.path == path)
                        return action;
            Debug.LogWarning("Can't find action with path: " + path);
            return null;
        }
    }
}