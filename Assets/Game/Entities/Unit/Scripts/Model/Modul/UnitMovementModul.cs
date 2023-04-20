using UnityEngine;
using StatSystem;

namespace Unit
{
    public class UnitMovementModul : UnitModelModul
    {
        private Rigidbody _body;
        private Stat _moveSpeed;
        private Stat _acc;

        public override void Tick ()
        {
            Vector2 move = Unit.Inputs.move;
            Vector3 moveV3 = new Vector3(move.x, 0, move.y);
            Vector3 velocity = _body.velocity;

            Vector3 targetVector = moveV3 * _moveSpeed.value;
            Vector3 deltaTarget = targetVector - velocity;

            _body.AddForce(deltaTarget * _acc.value, ForceMode.Acceleration);
        }

        protected override void OnInitialize ()
        {
            _body = Unit.Body;
            _moveSpeed = Unit.Stats.GetStat(StatType.MovementSpeed);
            _acc = Unit.Stats.GetStat(StatType.MovementAcceleration);
        }
    }
}