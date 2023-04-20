using UnityEngine;

namespace Unit
{
    public abstract class Target
    {
        public abstract Vector3 Position { get; }
    }

    public class GameObjectTarget : Target
    {
        public GameObject Object { get; private set; }

        public GameObjectTarget (GameObject gameObject) => Object = gameObject;

        public override Vector3 Position => Object.transform.position;
    }

    public class PositionTarget : Target
    {
        private Vector3 _position;

        public PositionTarget (Vector3 position) => _position = position;

        public override Vector3 Position => _position;
    }

    public class DamageableTarget : Target
    {
        protected Component component;
        public IDamageable Target => component as IDamageable;

        public DamageableTarget (IDamageable target)
        {
            component = target as Component;
        }

        public override Vector3 Position => component.transform.position;
    }

    public class UnitTarget : DamageableTarget
    {
        public UnitModel Unit => component as UnitModel;

        public UnitTarget (UnitModel unit) : base(unit)
        {
            component = unit;
        }
    }
}