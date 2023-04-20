using UnityEngine;
using TMPro;

namespace Session
{
    public class SessioneValuesView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _time;
        
        public void UpdateValues (SessionResult result)
        {
            if (_score != null)
                _score.text = result.score.ToString();

            if (_time != null)
            {
                int minutes = (int)result.duration.TotalMinutes;
                int seconds = result.duration.Seconds;
                _time.text = string.Format("{0}:{1}", minutes, seconds.ToString("F2"));
            }
        }
    }
}