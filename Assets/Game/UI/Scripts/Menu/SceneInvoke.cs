using UnityEngine.Events;

namespace UnityEngine.UI
{
    /// <summary> Alternative to use scene name, not as a string literal </summary>
    public class SceneInvoke : MonoBehaviour
    {
        public UnityEvent<string> OnInvoke;
        [SerializeField] private Key _scene;

        public void Invoke () => OnInvoke?.Invoke(_scene.Name);
    }
}