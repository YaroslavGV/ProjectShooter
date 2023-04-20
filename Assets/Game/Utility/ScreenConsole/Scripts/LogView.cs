using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LogsView
{
    public partial class LogView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _logString;
        [SerializeField] private TextMeshProUGUI _stackTrace;
        [SerializeField] private TextMeshProUGUI _counter;
        [SerializeField] private Graphic[] _colorTargets;
        [Space]
        [SerializeField] private LogTypeColors _colors;
        [Space]
        [SerializeField] private bool _defaultExpand = false;
        [SerializeField] private RectTransform _downArrow;
        private bool _isExpanded;

        public LogMassage Log { get; private set; }

        private void OnDestroy () => Unsubscribe();

        public void Set (LogMassage log)
        {
            Unsubscribe();

            Log = log;
            Log.OnIncrement += UpdateCounter;
            _isExpanded = _defaultExpand;

            Color color = _colors.GetColor(Log.Type);
            foreach (Graphic graphic in _colorTargets)
                graphic.color = color;

            UpdateText();
            UpdateCounter();
        }

        public void SetExpanded (bool status)
        {
            _isExpanded = status;
            UpdateText();
        }

        public void SwitchExpanded () => SetExpanded(_isExpanded == false);

        private void UpdateText ()
        {
            if (_logString != null)
            {
                string log;
                if (_isExpanded)
                {
                    int newLineIndex = Log.LogString.IndexOf('\n');
                    log = newLineIndex > 0 ? Log.LogString.Substring(0, newLineIndex) : Log.LogString;
                }
                else
                {
                    log = Log.LogString;
                }
                _logString.text = log;
            }
            if (_stackTrace != null)
            {
                bool visible = _isExpanded && string.IsNullOrEmpty(Log.StackTrace) == false;
                _stackTrace.gameObject.SetActive(visible);
                if (visible)
                    _stackTrace.text = Log.StackTrace;
            }
            if (_downArrow != null)
            {
                Vector3 scale = _downArrow.localScale;
                scale.y = Mathf.Abs(scale.y) * (_isExpanded ? -1 : 1);
                _downArrow.localScale = scale;
            }
        }

        private void UpdateCounter ()
        {
            if (_counter != null)
            {
                bool visible = Log.Count > 1;
                _counter.gameObject.SetActive(visible);
                if (visible)
                    _counter.text = Log.Count.ToString();
            }
        }

        private void Unsubscribe ()
        {
            if (Log != null)
                Log.OnIncrement -= UpdateCounter;
        }
    }
}