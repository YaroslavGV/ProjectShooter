using System;

namespace Memento
{
    public abstract class JsonMementroHandler
    {
        protected readonly string key;
        protected readonly IJsonContent target;
        protected readonly Func<string> getDefaultJson;

        public JsonMementroHandler (string key, IJsonContent target, Func<string> getDefaultJson = null, bool autoLoad = true)
        {
            this.key = key;
            this.target = target;
            this.target.ContentUpdated += Save;
            this.getDefaultJson = getDefaultJson;
            if (autoLoad)
                Load();
        }

        ~JsonMementroHandler () => target.ContentUpdated -= Save;

        public abstract void Load ();
        protected abstract void Save ();
        protected abstract void SetDefault ();
    }
}