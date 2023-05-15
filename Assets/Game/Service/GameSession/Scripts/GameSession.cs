using System;
using UnityEngine;

namespace Session
{
    public class GameSession : MonoBehaviour
    {
        public Action OnBagan;
        public Action<SessionResult> OnEnd;
        public Action OnPause;
        public Action OnResume;
        [SerializeField] private bool _beganOnStart;
        private Duration _duration;
        private Score _score;

        private void Start ()
        {
            if (_beganOnStart)
                Begin();
        }

        public Duration Duration => _duration;
        public bool IsPlaying { get; private set; }
        public bool IsPaused { get; private set; }
        public bool IsRunning => IsPlaying && IsPaused == false;
        public Score Score => _score;

        public void Begin ()
        {
            Debug.Log("GameSession Begin");
            _duration = new Duration();
            _score = new Score();
            IsPlaying = true;
            IsPaused = false;
            OnBagan?.Invoke();
        }

        public void End ()
        {
            if (IsPlaying == false)
            {
                Debug.LogWarning("Can`t end. Session not playing");
                return;
            }

            IsPlaying = false;
            IsPaused = false;
            _duration.Pause();

            SessionResult result = new SessionResult
            {
                duration = _duration.Passed,
                score = _score.Amount,
            };

            OnEnd?.Invoke(result);
        }

        public void Pause ()
        {
            Debug.Log("Pause");
            if (IsPlaying == false)
            {
                Debug.LogWarning("Can`t pause. Session not playing");
                return;
            }

            IsPaused = true;
            _duration.Pause();
            OnPause?.Invoke();
        }

        public void Resume ()
        {
            Debug.Log("Resume");
            if (IsPaused == false)
            {
                Debug.LogWarning("Can`t resume. Session not paused");
                return;
            }

            IsPaused = false;
            _duration.Resume();
            OnResume?.Invoke();
        }
    }
}