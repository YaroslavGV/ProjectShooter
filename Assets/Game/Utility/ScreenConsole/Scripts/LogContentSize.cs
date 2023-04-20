using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LogsView
{
    public class LogContentSize : MonoBehaviour
    {
        [Header("Padding")]
        [SerializeField] private float _top = 8;
        [SerializeField] private float _bot = 8;
        [SerializeField] private float _spacing = 8;
        [Space]
        private RectTransform _root;
        private LayoutElement[] _content;
        private bool _rebuildOnEnable;

        private void OnEnable ()
        {
            if (_rebuildOnEnable)
                StartCoroutine(LateDelay());
        }

        public void UpdateSize ()
        {
            if (_root == null)
            {
                _root = transform as RectTransform;
                _content = GetComponentsInChildren<LayoutElement>();
            }
            if (gameObject.activeInHierarchy)
                StartCoroutine(LateDelay());
            else
                _rebuildOnEnable = true;
        }

        private IEnumerator LateDelay ()
        {
            yield return new WaitForEndOfFrame();
            Rebuild();
        }

        private void Rebuild ()
        {
            float y = 0;

            y -= _top;
            foreach (var content in _content)
            {
                if (content.ignoreLayout || content.gameObject.activeSelf == false)
                    continue;

                RectTransform rect = content.transform as RectTransform;
                float height = content.preferredHeight;
                Vector2 size = rect.sizeDelta;
                size.y = height;
                rect.sizeDelta = size;

                Vector2 position = rect.anchoredPosition;
                position.y = y - (1 - rect.pivot.y) * height;

                y -= height + _spacing;
            }
            y -= _bot;

            Vector2 rootSize = _root.sizeDelta;
            rootSize.y = Mathf.Abs(y);
            _root.sizeDelta = rootSize;

            _rebuildOnEnable = false;
        }
    }
}