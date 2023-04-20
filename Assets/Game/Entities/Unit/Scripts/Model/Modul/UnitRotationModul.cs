using UnityEngine;
using StatSystem;

namespace Unit
{
    public class UnitRotationModul : UnitModelModul
    {
        private Stat _rotationSpeed;
        private Transform _rootTransform;

        public override void Tick ()
        {
            Vector2 direction = Unit.Inputs.direction;
            Vector2 move = Unit.Inputs.move;
            Vector2 target = Vector2.zero;

            if (direction != Vector2.zero)
                target = direction;
            else if (move != Vector2.zero)
                target = move;

            if (target != Vector2.zero)
            {
                Vector3 lookVector = new Vector3(target.x, 0, target.y);
                Quaternion targetRotation = Quaternion.LookRotation(lookVector);
                _rootTransform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed.value * Time.deltaTime);
            }
        }

        protected override void OnInitialize ()
        {
            _rootTransform = Unit.transform;
            _rotationSpeed = Unit.Stats.GetStat(StatType.RotationSpeed);
        }
    }
}