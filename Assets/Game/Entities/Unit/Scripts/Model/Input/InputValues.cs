using UnityEngine;

namespace Unit
{
    public struct InputValues
    {
        public Vector2 move;
        public Vector2 direction;
        public Target target;
        public float shootRange;

        public InputValues (Vector2 move, Vector2 direction) : this()
        {
            this.move = move;
            this.direction = direction;
        }
    }
}