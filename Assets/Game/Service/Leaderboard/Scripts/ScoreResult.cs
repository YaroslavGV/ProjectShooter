using System;

namespace LeaderboardSystem
{
    [Serializable]
    public struct ScoreResult
    {
        public int Score;
        public long Date;

        public override string ToString () => string.Format("{0} {1}", Score.ToString("N0"), GetDateTime().ToString("HH:mm dd:MM:y"));

        public ScoreResult (int score, DateTime date)
        {
            Score = score;
            Date = date.ToFileTimeUtc();
        }

        public DateTime GetDateTime () => DateTime.FromFileTimeUtc(Date);
    }
}