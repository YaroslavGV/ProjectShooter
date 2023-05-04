using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Memento;

namespace LeaderboardSystem
{
    public class MementoLeaderboard : Leaderboard, IJsonContent
    {
        [Serializable]
        private struct ScoreResultCollection
        {
            public ScoreResult[] elements;

            public ScoreResultCollection (IEnumerable<ScoreResult> results) => elements = results.ToArray();
        }

        public MementoLeaderboard (int limit) : base(limit) => OnChange += OnContentChange;

        ~MementoLeaderboard () => OnChange -= OnContentChange;

        public Action ContentUpdated { get; set; }

        public string GetJson () => JsonUtility.ToJson(new ScoreResultCollection(Results));

        public void SetJson (string json)
        {
            ScoreResultCollection resultCollection = JsonUtility.FromJson<ScoreResultCollection>(json);
            if (resultCollection.elements == null)
                return;
            results.Clear();
            results.AddRange(resultCollection.elements);
        }

        private void OnContentChange () => ContentUpdated?.Invoke();
    }
}