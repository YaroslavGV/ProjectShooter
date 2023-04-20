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
        private readonly List<ScoreResult> _results;
        
        public int ResultCount => _results.Count;
        public IEnumerable<ScoreResult> Results => _results;
        public ScoreResult LastResult { get; private set; }

        public override string ToString () => 
            string.Join(Environment.NewLine, _results.Select((r, index) => string.Format("{0}: {1}", index, r.ToString())));

        public Leaderboard (int limit = 10)
        {
            Limit = limit;
            _results = new List<ScoreResult>();
        }

        public void AddResult (int score) => AddResult(new ScoreResult(score, DateTime.Now));

        public void AddResult (ScoreResult result)
        {
            // Insert 0 for current result upped to top of same score results
            _results.Insert(0, result);
            _results.Sort(ResultComparison);
            if (_results.Count > Limit)
                _results.RemoveRange(Limit, _results.Count - Limit);

            LastResult = result;
            OnChange?.Invoke();
        }

        public void SetResults (IEnumerable<ScoreResult> results)
        {
            _results.Clear();
            _results.AddRange(results);
        }

        private int ResultComparison (ScoreResult resultA, ScoreResult resultB) => resultB.Score - resultA.Score;
    }
}