using UnityEngine;
using UnityEngine.UI;

namespace Session
{
    [RequireComponent(typeof(GameSession))]
    public class SessionResultHandler : MonoBehaviour
    {
        [SerializeField] private SessioneValuesView _resultView;
        [SerializeField] private CanvasGroupRepresent _resultWindow;
        [SerializeField] private CanvasGroupRepresent _sticks;
        private GameSession _session;
        
        private void Start ()
        {
            _session = GetComponent<GameSession>();
            _session.OnEnd += OnSessionEnd;
        }

        private void OnSessionEnd (SessionResult result)
        {
            _resultView.UpdateValues(result);
            if (_resultWindow != null)
                _resultWindow.Show();
            if (_sticks != null)
                _sticks.Hide();
        }
    }
}