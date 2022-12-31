using Newtonsoft.Json;
using PlayFab.ConfigDatas;
using PlayFab.ServerModels;
using ScriptableObjects.ConfigData;
using UnityEngine;

namespace PlayFab.ServerServices.GameLevel
{
    public class SendLevelsService
    {
        public void SendLevels( LevelVO levels)
        {
            var request = new SetTitleDataRequest 
            {
                Key = "Levels",
                Value = JsonConvert.SerializeObject(levels)
                
            };
                
                
            PlayFabServerAPI.SetTitleData(request,OnDataSend,PlayFabErrorMessage.OnError);
        }
        
        private void OnDataSend(SetTitleDataResult result)
        {
            Debug.Log("Success");
        }
    }
}