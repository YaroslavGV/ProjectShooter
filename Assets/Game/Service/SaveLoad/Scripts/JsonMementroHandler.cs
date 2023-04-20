namespace Memento
{
    public abstract class JsonMementroHandler
    {
        protected readonly string key;
        protected readonly IJsonContent target;

        public JsonMementroHandler (string key, IJsonContent target, bool autoLoad = true)
        {
            this.key = key;
            this.target = target;
            this.target.ContentUpdated += Save;
            if (autoLoad)
                Load();
        }

        ~JsonMementroHandler () => target.ContentUpdated -= Save;

        public abstract void Load ();
        protected abstract void Save ();

        protected abstract void SetDefault ();
    }
}