using System;
using System.Linq;
using System.Collections.Generic;

namespace LeaderboardSystem
{
    [Serializable]
    public class Leaderboard
    {
        public Action OnChange;
        public readonly int Limit = 10;
        protected readonly List<ScoreResult> results;
        
        public int ResultCount => results.Count;
        public IEnumerable<ScoreResult> Results => results;
        public ScoreResult LastResult { get; private set; }

        public override string ToString () => 
            string.Join(Environment.NewLine, results.Select((r, index) => string.Format("{0}: {1}", index, r.ToString())));

        public Leaderboard (int limit = 10)
        {
            Limit = limit;
            results = new List<ScoreResult>();
        }

        public void AddResult (int score) => AddResult(new ScoreResult(score, DateTime.Now));

        public void AddResult (ScoreResult result)
        {
            // Insert 0 for current result upped to top of same score results
            results.Insert(0, result);
            results.Sort(ResultComparison);
            if (results.Count > Limit)
                results.RemoveRange(Limit, results.Count - Limit);

            LastResult = result;
            OnChange?.Invoke();
        }

        private int ResultComparison (ScoreResult resultA, ScoreResult resultB) => resultB.Score - resultA.Score;
    }
}