using System.Collections.Generic;
using UnityEngine;

namespace LogsView
{
    public class ConsoleView : MonoBehaviour
    {
        [SerializeField] private LogView _logTemplate;
        [SerializeField] private Transform _parent;
        [Space]
        [SerializeField] private GameObject _view;
        private LogMassages _log;
        private List<LogView> _views = new List<LogView>();

        public void ToggleViewVisible () => _view.SetActive(_view.activeSelf == false);

        private void OnEnable ()
        {
            if (_log == null)
            {
                _log = LogMassagesSingletom.Instance;
                _log.OnAdd += AddLog;
                _log.OnRemove += RemoveLog;
                UpdateAllLogs();
            }
        }

        private void OnDestroy ()
        {
            if (_log == null)
            {
                _log.OnAdd -= AddLog;
                _log.OnRemove -= RemoveLog;
            }
        }

        public void ClearLogs ()
        {
            _log.ClearLogs();
            foreach (var log in _views)
                Destroy(log.gameObject);
            _views.Clear();
        }

        private void UpdateAllLogs ()
        {
            foreach (LogMassage log in _log.Massages)
                AddLog(log);
        }

        private void AddLog (LogMassage log)
        {
            LogView logView = Instantiate(_logTemplate, _parent);
            logView.Set(log);
            _views.Add(logView);
        }

        private void RemoveLog (LogMassage log)
        {
            foreach (LogView view in _views)
                if (view.Log == log)
                {
                    _views.Remove(view);
                    Destroy(view.gameObject);
                    break;
                }
        }
    }
}