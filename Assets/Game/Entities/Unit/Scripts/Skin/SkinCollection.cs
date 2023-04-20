using System.Collections.Generic;
using UnityEngine;

namespace Unit.Skin
{
    [CreateAssetMenu(fileName = "SkinCollection", menuName = "Unit/SkinCollection")]
    public class SkinCollection : ScriptableObject
    {
        [SerializeField] private UnitSkin[] _elements;

        public int Count => _elements.Length;
        public UnitSkin this[int i] => _elements[i];

        public IEnumerable<UnitSkin> Elements => _elements;

        [ContextMenu("Check Key Valid")]
        public void CheckKeyValid ()
        {
            ChackKeyMissing();
            CheckKeyCollision();
        }

        private void ChackKeyMissing ()
        {
            for (int i = 0; i < _elements.Length; i++)
                if (string.IsNullOrEmpty(_elements[i].Key))
                    Debug.LogWarning(string.Format("Skin key missing on index {0}", i));
        }

        private void CheckKeyCollision ()
        {
            for (int i = 0; i < _elements.Length; i++)
                for (int j = i + 1; j < _elements.Length; j++)
                    if (string.IsNullOrEmpty(_elements[i].Key) == false && _elements[i].Key == _elements[j].Key)
                        Debug.LogWarning(string.Format("Skin key collision \"{0}\" on index {1} and {2}", _elements[i].Key, i, j));
        }
    }
}