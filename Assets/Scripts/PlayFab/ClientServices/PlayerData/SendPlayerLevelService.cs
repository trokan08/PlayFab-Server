using System.Collections.Generic;
using GamePlay.Enums;
using Newtonsoft.Json;
using PlayFab.ClientModels;
using PlayFab.ConfigDatas;
using UnityEngine;

namespace PlayFab.ClientServices.PlayerNameInformation
{
    public class SendPlayerLevelService
    {

        public void SendPlayerData(LevelName currentLevel)
        {
            var request = new UpdateUserDataRequest()
            {
                Data = new Dictionary<string, string>()
                {
                    {"CurrentLevel", JsonConvert.SerializeObject(currentLevel)}
                }
            };
            PlayFabClientAPI.UpdateUserData(request,OnLevelDataSended,PlayFabErrorMessage.OnError);
        }

        private void OnLevelDataSended(UpdateUserDataResult result)
        {
            Debug.Log("Player level data sended");
        }
        
        
    }
}