using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unit.Skin
{
    public class PlayerSkins
    {
        public Action OnChanged;
        private readonly UnitSkin[] _collection;
        private List<string> _available;
        private string _selected;
        
        public PlayerSkins (IEnumerable<UnitSkin> collection, UnitSkin defaultSkin = null)
        {
            _collection = collection.ToArray();
            _available = new List<string>();
            if (defaultSkin == null)
                defaultSkin = _collection[0];
            SetAvailable(defaultSkin);
            SelectSkin(defaultSkin);
        }

        public override string ToString () =>
            string.Format("Selected: {0}\nAvailable: {1}", _selected, string.Join(", ", _available));

        public int Count => _collection.Length;
        public IEnumerable<UnitSkin> Skins => _collection;
        public UnitSkin SelectedSkin => GetSkin(_selected);
        public IEnumerable<string> AvailableSkins => _available;

        public void SelectSkin (UnitSkin skin)
        {
            if (_collection.Contains(skin))
            {
                _selected = skin.Key;
                OnChanged?.Invoke();
            }
            else
                Debug.LogWarning("Can`t select, player skins collection not contain "+ skin.Key);
        }

        public void SetAvailable (UnitSkin skin)
        {
            if (_collection.Contains(skin))
            {
                if (IsAvailable(skin))
                    return;
                _available.Add(skin.Key);
                OnChanged?.Invoke();
            }
            else
                Debug.LogWarning("Can't set available, player skins collection not contain " + skin.Key);
        }

        public void SelectSkin (string key)
        {
            UnitSkin skin = GetSkin(key);
            if (skin != null)
                SelectSkin(skin);
            else
                Debug.LogWarning("Skin with key " + key + " is missing on collection");
        }

        public void SetAvailable (string key)
        {
            UnitSkin skin = GetSkin(key);
            if (skin != null)
                SetAvailable(skin);
            else
                Debug.LogWarning("Skin with key " + key + " is missing on collection");
        }

        public bool IsAvailable (UnitSkin skin) => _available.Contains(skin.Key);

        private UnitSkin GetSkin (string key) => _collection.FirstOrDefault(s => s.Key == key);
    }
}