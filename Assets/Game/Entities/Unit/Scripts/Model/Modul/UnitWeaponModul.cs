using System.Collections.Generic;
using UnityEngine;
using Zenject;
using InventorySystem;
using Weapon;

namespace Unit
{
    using Skin;

    public class UnitWeaponModul : UnitModelModul
    {
        [SerializeField] private ShootAgent[] _shootAgents;
        private const float ShootTriggerValue = 0.95f;
        [Inject] private DiContainer _container;
        private IDamageSorce _damageSorce;
        private Dictionary<string, WeaponModel> _pointWeapon;
        private bool _isPrimaryShooting;
        
        public override void Tick ()
        {
            Vector2 aim = Unit.Inputs.direction;
            bool isShooting = aim.sqrMagnitude > ShootTriggerValue * ShootTriggerValue;
            if (_isPrimaryShooting != isShooting)
            {
                _isPrimaryShooting = isShooting;
                SetPrimaryShooting(_isPrimaryShooting);
            }
        }

        public override void OnDead ()
        {
            SetPrimaryShooting(false);
        }

        public void Equipe (WeaponModel weapon, string pointName)
        {
            if (_pointWeapon.ContainsKey(pointName))
            {
                WeaponModel currentWeapon = _pointWeapon[pointName];
                Destroy(currentWeapon.gameObject);
                currentWeapon.OnShoot -= OnWeaponShoot;
                _pointWeapon.Remove(pointName);
            }
            Transform parent = Unit.Skin.GetPoint(pointName);
            WeaponModel newWeapon = _container.InstantiatePrefabForComponent<WeaponModel>(weapon, parent.position, parent.rotation, parent);
            newWeapon.SetDamageSorce(_damageSorce);
            newWeapon.OnShoot += OnWeaponShoot;
            _pointWeapon.Add(pointName, newWeapon);
        }

        public float GetMinTargetRange ()
        {
            if (_pointWeapon.Count > 0)
            {
                float range = Mathf.Infinity;
                foreach (var slotWeapon in _pointWeapon)
                    if (range > slotWeapon.Value.TargetRange)
                        range = slotWeapon.Value.TargetRange;
                return range;
            }
            else
            {
                return 0;
            }
        }

        protected override void OnInitialize ()
        {
            _damageSorce = Unit;
            _pointWeapon = new Dictionary<string, WeaponModel>();

            foreach (SlotItem slotItem in Unit.Profile.equipment.items)
            {
                if (slotItem.item is WeaponItem wItem)
                    Equipe(wItem.Model, slotItem.slot);
                else
                    Debug.LogWarning("Item is not \"WeaponItem\" type.");
            }
        }

        private void OnWeaponShoot (WeaponModel weapon)
        {
            foreach (var pw in _pointWeapon)
                if (pw.Value == weapon)
                {
                    foreach (ShootAgent agent in _shootAgents)
                    {
                        if (agent.Key == pw.Key)
                            agent.Shoot();
                    }
                    break;
                }
        }

        private void SetPrimaryShooting (bool isShooting)
        {
            foreach (var weapon in _pointWeapon)
                if (weapon.Value.Type == WeaponType.Primary)
                    weapon.Value.SetShooting(isShooting);
        }
    }
}