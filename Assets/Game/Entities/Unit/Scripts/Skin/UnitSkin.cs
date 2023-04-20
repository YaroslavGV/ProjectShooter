using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Unit.Skin
{
    public class UnitSkin : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Point[] _points;

        public string Key => _key;
        public Sprite Icon => _icon;
        public IEnumerable<Point> Points => _points;

        public Transform GetPoint (string pointName)
        {
            Point point = _points.FirstOrDefault(p => p.key.Name == pointName);
            if (point.transform != null)
                return point.transform;
            Debug.LogWarning(string.Format("{0} point {1} is missing", name, pointName));
            return transform;
        }
    }
}