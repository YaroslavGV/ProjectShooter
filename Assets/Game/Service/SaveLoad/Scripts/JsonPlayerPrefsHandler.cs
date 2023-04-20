using UnityEngine;

namespace Memento
{
    public class JsonPlayerPrefsHandler : JsonMementroHandler
    {
        public JsonPlayerPrefsHandler (string key, IJsonContent target, bool autoLoad = true) : 
            base(key, target, autoLoad)
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
            target.SetDefault();
        }
    }
}