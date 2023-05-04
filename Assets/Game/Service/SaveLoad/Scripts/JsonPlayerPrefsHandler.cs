using System;
using UnityEngine;

namespace Memento
{
    public class JsonPlayerPrefsHandler : JsonMementroHandler
    {
        public JsonPlayerPrefsHandler (string key, IJsonContent target, Func<string> getDefaultJson = null, bool autoLoad = true) : 
            base(key, target, getDefaultJson, autoLoad)
        {
        }

        public override void Load ()
        {
            if (PlayerPrefs.HasKey(key))
            {
                string json = PlayerPrefs.GetString(key);
                target.SetJson(json);
            } 
            else
            {
                SetDefault();
            }
        }

        protected override void Save ()
        {
            string json = target.GetJson();
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }

        protected override void SetDefault ()
        {
            if (getDefaultJson != null)
                target.SetJson(getDefaultJson());
        }
    }
}