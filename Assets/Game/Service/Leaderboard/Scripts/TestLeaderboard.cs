using UnityEngine;
using Zenject;

namespace LeaderboardSystem
{
    public class TestLeaderboard : MonoBehaviour
    {
        [SerializeField] private int _score = 100;
        [Inject] private Leaderboard _leaderboard;

        [ContextMenu("Add Result")]
        private void AddResult () => _leaderboard.AddResult(_score);

        [ContextMenu("Log Table")]
        private void LogTable () => Debug.Log(_leaderboard);
    }
}