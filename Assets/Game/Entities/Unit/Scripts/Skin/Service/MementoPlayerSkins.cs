using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Memento;

namespace Unit.Skin
{
    public class MementoPlayerSkins : PlayerSkins, IJsonContent
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

        public MementoPlayerSkins (IEnumerable<UnitSkin> collection, string defaultSkin = "") : base(collection, defaultSkin) => OnChanged += OnContentChange;

        ~MementoPlayerSkins () => OnChanged -= OnContentChange;

        public Action ContentUpdated { get; set; }

        public string GetJson () => JsonUtility.ToJson(new SkinSaveData(available, selected));

        public void SetJson (string json)
        {
            SkinSaveData saveData = JsonUtility.FromJson<SkinSaveData>(json);
            if (saveData.available == null)
                return;

            available.Clear();
            available.AddRange(saveData.available);
            selected = saveData.selected;
        }

        private void OnContentChange () => ContentUpdated?.Invoke();
    }
}