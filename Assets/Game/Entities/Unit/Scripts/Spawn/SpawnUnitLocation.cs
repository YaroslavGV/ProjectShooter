using UnityEngine;
using FractionSystem;
using PointGenerator;

namespace Unit
{
    public class SpawnUnitLocation : MonoBehaviour
    {
        private enum SpawnRotation { randomY, transfrormY }
        [SerializeField] private SpawnRotation _rotation;
        [Tooltip("Use self position if location is null")]
        [SerializeField] private PointLocation _location;
        [SerializeField] private SceneUnits _units;

        public UnitModel SpawnUnit (UnitProfile profile, Fraction fraction) 
        {
            UnitModel unit = _units.SpawnUnit(profile, fraction, GetPoint(), GetRotation());
            return unit;
        }

        private Vector3 GetPoint () => _location != null ? _location.GetPoint() : transform.position;

        private Quaternion GetRotation ()
        {
            if (_rotation == SpawnRotation.randomY)
                return GetRandomRotation();
            if (_rotation == SpawnRotation.transfrormY)
                return GetRotationFromY(transform.localEulerAngles.y * Mathf.Deg2Rad);
            return transform.rotation;
        }

        private Quaternion GetRotationFromY (float radian)
        {
            Vector3 vector = new Vector3(Mathf.Cos(radian), 0, Mathf.Sin(radian));
            return Quaternion.LookRotation(vector);
        }

        private Quaternion GetRandomRotation () => GetRotationFromY(Random.Range(0, Mathf.PI * 2f));
    }
}