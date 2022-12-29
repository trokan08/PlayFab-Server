using System;
using System.Collections.Generic;
using GameManager;
using GamePlay.Enums;
using Newtonsoft.Json;
using PlayFab.ClientModels;
using PlayFab.ConfigDatas;
using UnityEngine;

namespace PlayFab.ClientServices.PlayerNameInformation
{
    public class GetPlayerLevelService
    {
        private Action<PlayerLevel> _action;
        public void GetPlayerLevel( Action<PlayerLevel> action)
        {
            _action = action;
            List<string> levels = new List<string>(){"CurrentLevel"};

            var request = new GetUserDataRequest()
            {
                PlayFabId = GameStatistics.PlayFabID,
                Keys = levels
            };
            
            PlayFabClientAPI.GetUserData(request,OnPlayerLevel,PlayFabErrorMessage.OnError);
        }

        private void OnPlayerLevel(GetUserDataResult result)
        {
            PlayerLevel playerLevel = new PlayerLevel();
            if (result != null && result.Data.ContainsKey("CurrentLevel"))
            {
                playerLevel.IsFirstTime = false;
                Debug.Log(result.Data["CurrentLevel"]);
                playerLevel.CurrentLevel =JsonConvert.DeserializeObject<LevelName>(result.Data["CurrentLevel"].Value);;
            }
            else
            {
                playerLevel.IsFirstTime = true;
            }
            _action.Invoke(playerLevel);

            
        }
        
        
    }

    public class PlayerLevel
    {
        public bool IsFirstTime;
        public LevelName CurrentLevel;
    }
}