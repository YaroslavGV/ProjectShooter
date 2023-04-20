using UnityEngine;
using LeaderboardSystem;
using Zenject;

namespace Session
{
    [RequireComponent(typeof(GameSession))]
    public class SessionResultLeaderboard : MonoBehaviour
    {
        private GameSession _session;
        private Leaderboard _leaderboard;

        private void Start ()
        {
            _session = GetComponent<GameSession>();
            _session.OnEnd += WriterResult;
        }

        private void OnDestroy ()
        {
            _session.OnEnd -= WriterResult;
        }

        [Inject]
        public void SetLeaderboard (Leaderboard leaderboard)
        {
            _leaderboard = leaderboard;
        }

        private void WriterResult (SessionResult result)
        {
            int score = result.score;
            if (score > 0)
                _leaderboard.AddResult(score);
        }
    }
}