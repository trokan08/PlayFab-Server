using System.Collections.Generic;
using Newtonsoft.Json;
using PlayFab.ClientModels;
using PlayFab.ConfigDatas;
using UnityEngine;

namespace PlayFab.ClientServices.GameAnalytics
{
    public class LevelInformationEvent
    {
        public void LevelDuration(LevelVO level)
        {
            var request = new WriteClientPlayerEventRequest
            {
                EventName = "LevelInformation",
                Body = new Dictionary<string, object>
                {
                    {"LevelInformation",JsonConvert.SerializeObject(level)}
                }
                

            };
              
            
            PlayFabClientAPI.WritePlayerEvent(request,OnLevelDurationSended,PlayFabErrorMessage.OnError);
        }

        private void OnLevelDurationSended(WriteEventResponse response)
        {
            Debug.Log("Level Duration information send");
        }
       
    }

    public class LevelVO
    {
        public float Minute;
        public float Seconds;
        public string LevelName;
        public int CoinCount;
        public int CollidedObstacleCount;

    }
}