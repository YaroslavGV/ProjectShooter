using UnityEngine;
using UnityEngine.Events;

namespace Stick
{
    public class StickEvents : StickInput
    {
        [Space]
        public UnityEvent<Vector2> OnInput;
        public UnityEvent OnRelese;
        private bool _input;

        protected override void Update ()
        {
            base.Update();
            if (_input)
            {
                if (Axis != Vector2.zero)
                {
                    OnInput?.Invoke(Axis);
                }
                else
                {
                    _input = false;
                    OnRelese?.Invoke();
                }
            }
            else if (Axis != Vector2.zero)
            {
                _input = true;
                OnInput?.Invoke(Axis);
            }
        }
    }
}