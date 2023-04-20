using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class BotInputHandler : MonoBehaviour
    {
        [SerializeField] private SceneUnits _units;
        private List<UnitModel> _unit = new List<UnitModel>();

        private void Update ()
        {
            for (int i = _unit.Count - 1; i > -1; i--)
            {
                if (_unit[i].IsAlive == false)
                    _unit.RemoveAt(i);
                else
                    UpdateUnit(_unit[i]);
            }
        }

        public void AddUnit (UnitModel unit)
        {
            UnitModel target = _units.FindEnemyFor(unit);
            if (target != null)
            {
                UnitWeaponModul weapon = target.GetModul<UnitWeaponModul>();
                InputValues values = unit.Inputs;
                values.target = new UnitTarget(target);
                if (weapon != null)
                    values.shootRange = weapon.GetMinTargetRange();
                unit.Inputs = values;
            }
            _unit.Add(unit);
        }

        public void UpdateUnit (UnitModel unit)
        {
            InputValues values = unit.Inputs;

            if (values.target == null)
                return;

            IDamageable damagebleTarget = values.target is DamageableTarget dTarget ? dTarget.Target : null;
            if (damagebleTarget != null && damagebleTarget.IsAlive == false)
            {
                values.move = Vector2.zero;
                values.direction = Vector2.zero;
                values.target = null;
            }
            else
            {
                Vector3 targetVectorV3 = values.target.Position - unit.transform.position;
                Vector2 targetVector = new Vector2(targetVectorV3.x, targetVectorV3.z);
                float sqrDistance = targetVector.sqrMagnitude;
                Vector2 normalVector = targetVector.normalized;
                float sqrShootRange = values.shootRange * values.shootRange;

                bool move = sqrDistance > sqrShootRange;
                values.move = move ? normalVector : Vector2.zero;

                if (damagebleTarget != null)
                {
                    bool shoot = sqrDistance < sqrShootRange;
                    values.direction = shoot ? normalVector : Vector2.zero;
                }
                else
                    values.direction = Vector2.zero;
            }

            unit.Inputs = values;
        }
    }
}