using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LeaderboardSystem.UI
{
    public class ScoreResultRow : MonoBehaviour
	{
		[SerializeField] private string _scoreFormat = "N0";
		[SerializeField] private string _dateFormat = "dd.MM.y HH:mm";
		[Space]
		[SerializeField] private string _missIndex = "-";
		[SerializeField] private string _missScore = "--- --- ---";
		[SerializeField] private string _missDate = "--.--.-- --:--";
		[Space]
		[SerializeField] private TextMeshProUGUI _index;
		[SerializeField] private TextMeshProUGUI _score;
		[SerializeField] private TextMeshProUGUI _date;
		[Space]
		[SerializeField] private Graphic[] _colorTargets;

		public void SetMissData ()
		{
			_index.text = _missIndex;
			_score.text = _missScore;
			_date.text = _missDate;
		}

		public void SetData (ScoreResult date, int index)
		{
			_index.text = index.ToString();
			_score.text = date.Score.ToString(_scoreFormat);
			DateTime dateTime = date.GetDateTime();
			_date.text = dateTime.ToString(_dateFormat);
		}

		public void SetColor (Color color)
		{
			foreach (Graphic graphic in _colorTargets)
				graphic.CrossFadeColor(color, 0.01f, true, true);
		}
	}
}
