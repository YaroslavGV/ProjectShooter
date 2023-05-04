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
        protected List<string> available;
        protected string selected;
        
        public PlayerSkins (IEnumerable<UnitSkin> collection, string defaultSkin = "")
        {
            _collection = collection.ToArray();
            available = new List<string>();
            if (string.IsNullOrEmpty(defaultSkin))
                defaultSkin = _collection[0].Key;
            SetAvailable(defaultSkin);
            SelectSkin(defaultSkin);
        }

        public override string ToString () =>
            string.Format("Selected: {0}\nAvailable: {1}", selected, string.Join(", ", available));

        public int Count => _collection.Length;
        public IEnumerable<UnitSkin> Skins => _collection;
        public UnitSkin SelectedSkin => GetSkin(selected);
        public IEnumerable<string> AvailableSkins => available;

        public void SelectSkin (UnitSkin skin)
        {
            if (_collection.Contains(skin))
            {
                selected = skin.Key;
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
                available.Add(skin.Key);
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

        public bool IsAvailable (UnitSkin skin) => available.Contains(skin.Key);

        private UnitSkin GetSkin (string key) => _collection.FirstOrDefault(s => s.Key == key);
    }
}