using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UI
{
    public class MenuSwitchController : MonoBehaviour
    {
        public Action<Key> OnHide;
        public Action<Key> OnShow;
        [SerializeField] private float _delay = 0.1f;
        [Space]
        [SerializeField] private Key _default;
        [SerializeField] private Transform[] _findAgentsFromParent;
        private List<MenuSwitchAgent> _agents;
        private Key _current;

        private void Start ()
        {
            _agents = new List<MenuSwitchAgent>();
            foreach (Transform parent in _findAgentsFromParent)
                _agents.AddRange(parent.GetComponentsInChildren<MenuSwitchAgent>(true));
        
            if (_default != null)
                SwitchTo(_default);
        }

        public void SwitchTo (Key key)
        {
            if (_current != key)
            {
                _current = key;
                Hide();
                if (_delay > 0)
                {
                    StopAllCoroutines();
                    StartCoroutine(DelayProcess());
                }
                else
                {
                    Show();
                }
            }
        }

        private IEnumerator DelayProcess ()
        {
            yield return new WaitForSecondsRealtime(_delay);
            Show();
        }

        private void Show ()
        {
            foreach (MenuSwitchAgent agent in _agents)
                agent.Show(_current);
            OnShow?.Invoke(_current);
        }

        private void Hide ()
        {
            foreach (MenuSwitchAgent agent in _agents)
                agent.Hide(_current);
            OnHide?.Invoke(_current);
        }
    }
}