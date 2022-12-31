using System;
using System.Collections;
using PlayFab.ClientServices.GameAnalytics;
using UnityEngine;

namespace GameManager
{
    public class GameAnalyticManager : MonoBehaviour
    {
        private void Start()
        {
            GameActions.Instance.SaveGame += SendLevelData;
            Screen.fullScreen = !Screen.fullScreen;


        }

        private void OnDisable()
        {
            GameActions.Instance.SaveGame -= SendLevelData;

        }

       
        
        private void SendLevelData()
        {
            LevelVO levelInfo = GameActions.Instance.PlayedLevel.Invoke();
            levelInfo.CoinCount = GameActions.Instance.CoinCount.Invoke();
            levelInfo.CollidedObstacleCount = GameActions.Instance.ObstacleCollidedCount.Invoke();
            LevelInformationEvent levelInformationEvent = new LevelInformationEvent();
            levelInformationEvent.LevelDuration(levelInfo);
        }

    }
}