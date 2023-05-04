using System;

namespace Memento
{
    public interface IJsonContent
    {
        public Action ContentUpdated { get; set; }
        public void SetJson (string json);
        public string GetJson ();
    }
}