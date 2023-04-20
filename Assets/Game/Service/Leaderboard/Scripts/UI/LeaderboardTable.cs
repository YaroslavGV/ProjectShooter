using UnityEngine;
using Zenject;

namespace LeaderboardSystem.UI
{
    public class LeaderboardTable : MonoBehaviour
	{
		[SerializeField] private Color _normalColor = Color.white;
		[SerializeField] private Color _missColor = Color.gray;
		[SerializeField] private Color _lastResultColor = Color.cyan;
		[Space]
		[SerializeField] private ScoreResultRow _template;
		private ScoreResultRow[] _rows;
		private Leaderboard _leaderboard;

		private void OnEnable ()
		{
			if (_leaderboard != null)
				UpdateList();
		}

		[Inject]
		public void SetLeaderboard (Leaderboard leaderboard) 
		{
			_leaderboard = leaderboard;
			int count = _leaderboard.Limit;
			_rows = new ScoreResultRow[count];
			for (int i = 0; i < count; i++)
            	_rows[i] = Instantiate(_template, transform);
		}

		public void UpdateList ()
		{
			foreach (var elements in _rows)
			{
				elements.SetMissData();
				elements.SetColor(_missColor);
			}
			int index = 0;
			foreach (ScoreResult result in _leaderboard.Results)
			{
				if (index >= _rows.Length)
					break;
				_rows[index].SetData(result, index + 1);
				_rows[index].SetColor(_normalColor);
				if (result.Date == _leaderboard.LastResult.Date)
					_rows[index].SetColor(_lastResultColor);
				index++;
			}
		}
	}
}