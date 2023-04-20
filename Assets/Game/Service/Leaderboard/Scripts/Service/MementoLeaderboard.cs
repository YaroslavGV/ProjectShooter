using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Memento;

namespace LeaderboardSystem
{
    public class MementoLeaderboard : IJsonContent
    {
        [Serializable]
        private struct ScoreResultCollection
        {
            public ScoreResult[] elements;

            public ScoreResultCollection (IEnumerable<ScoreResult> results) => elements = results.ToArray();
        }

        private readonly Leaderboard _leaderboard;

        public MementoLeaderboard (Leaderboard leaderboard) 
        {
            _leaderboard = leaderboard;

            _leaderboard.OnChange += OnChange; 
        }

        ~MementoLeaderboard () => _leaderboard.OnChange -= OnChange;

        public Action ContentUpdated { get; set; }

        public string GetJson () => JsonUtility.ToJson(new ScoreResultCollection(_leaderboard.Results));

        public void SetJson (string json)
        {
            ScoreResultCollection results = JsonUtility.FromJson<ScoreResultCollection>(json);
            _leaderboard.SetResults(results.elements);
        }

        public void SetDefault () { }

        private void OnChange () => ContentUpdated?.Invoke();
    }
}