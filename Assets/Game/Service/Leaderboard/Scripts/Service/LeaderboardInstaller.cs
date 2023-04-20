using System;
using UnityEngine;
using Zenject;
using Memento;

namespace LeaderboardSystem
{
    public class LeaderboardInstaller : MonoInstaller
    {
        [SerializeField] private string _saveKey = "Leaderboard";
        [SerializeField] private int _limit = 10;
        [Space]
        [SerializeField] private bool _log = true;

        public override void InstallBindings ()
        {
            if (string.IsNullOrEmpty(_saveKey))
                throw new Exception("SaveKey is null or empty");
            if (_limit < 1)
                throw new Exception("Limit must be a positive value");

            Leaderboard leaderboard = new Leaderboard(_limit);
            Container.Bind<Leaderboard>().FromInstance(leaderboard).AsSingle();

            MementoLeaderboard mLeaderboard = new MementoLeaderboard(leaderboard);
            new JsonPlayerPrefsHandler(_saveKey, mLeaderboard);

            if (_log)
            {
                string text = ObjectLog.GetText(leaderboard, _saveKey);
                Debug.Log(text);
            }
        }

        [ContextMenu("ClearData")]
        private void ClearData ()
        {
            PlayerPrefs.DeleteKey(_saveKey);
            PlayerPrefs.Save();
        }
    }
}