using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Memento;

namespace Unit.Skin
{
    public class MementoPlayerSkins : IJsonContent
    {
        [Serializable]
        private struct SkinSaveData
        {
            public string[] available;
            public string selected;

            public SkinSaveData (IEnumerable<string> available, string selected)
            {
                this.available = available.ToArray();
                this.selected = selected;
            }

            public override string ToString () => "available: " + string.Join(", ", available) + Environment.NewLine + "selected:" + selected;
        }

        private readonly PlayerSkins _skins;

        public MementoPlayerSkins (PlayerSkins skins)
        {
            _skins = skins;

            _skins.OnChanged += OnChange;
        }

        ~MementoPlayerSkins () => _skins.OnChanged -= OnChange;

        public Action ContentUpdated { get; set; }

        public string GetJson ()
        {
            SkinSaveData saveData = new SkinSaveData(_skins.AvailableSkins, _skins.SelectedSkin.Key);
            return JsonUtility.ToJson(saveData);
        }

        public void SetJson (string json)
        {
            SkinSaveData saveData = JsonUtility.FromJson<SkinSaveData>(json);
            if (saveData.available == null)
                return;
            
            foreach (string skinKey in saveData.available)
                _skins.SetAvailable(skinKey);
            _skins.SelectSkin(saveData.selected);
        }

        public void SetDefault () { }

        private void OnChange () => ContentUpdated?.Invoke();
    }
}