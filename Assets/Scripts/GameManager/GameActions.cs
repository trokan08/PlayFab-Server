using System;
using NaughtyAttributes;
using PlayFab.ClientServices.GameAnalytics;
using UnityEngine;

namespace GameManager
{
    public class GameActions : MonoBehaviour
    {
        public static GameActions Instance;
        

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public Action<Action> StartGame;
        public Action<Vector3> JoyStickDirection;
        public Action CoinCollected;
        public Action GameSucces;
        public Action SaveGame;
        public Action<Action> OpenLeaderBoard;
        public Action<int> SetGameFinishUI;
        public Action<string> SetUsername;
        public Action<string> GetUsername;
        public Action<Action> NextLevel;
        public Action UIReload;
        public Func<int> CoinCount;
        public Func<int> ObstacleCollidedCount;
        public Func<LevelVO> PlayedLevel;

    }
}