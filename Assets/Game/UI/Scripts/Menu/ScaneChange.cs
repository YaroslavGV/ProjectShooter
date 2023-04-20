using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UnityEngine.UI
{
    public class ScaneChange : MonoBehaviour
    {
        [SerializeField] private FadeTarget[] _fadeTargets;
        public UnityEvent OnStartFade;
        
        public void GoToScene (string scene)
        {
            StopAllCoroutines();
            StartCoroutine(DelayProcess(scene));
            OnStartFade?.Invoke();
        }

        private IEnumerator DelayProcess (string scene)
        {
            float delay = 0;
            foreach (FadeTarget fade in _fadeTargets)
            {
                if (delay < fade.target.OutDuration)
                    delay = fade.target.OutDuration;
                fade.target.DoFadeAction(fade.action);
            }
            yield return new WaitForSecondsRealtime(delay);
            SceneManager.LoadScene(scene);
        }
    }
}