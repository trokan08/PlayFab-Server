using Newtonsoft.Json;
using PlayFab.ConfigDatas;
using PlayFab.ServerModels;
using ScriptableObjects.ConfigData;
using UnityEngine;

namespace PlayFab.ServerServices.GameLevel
{
    public class SendLevelDesignService
    {
        public void SendLevelInformation( LevelDesignVO levelDesign)
        {
            var request = new SetTitleDataRequest 
            {
                Key = levelDesign.LevelName,
                Value = JsonConvert.SerializeObject(levelDesign)
                
            };
                
                
            PlayFabServerAPI.SetTitleData(request,OnDataSend,PlayFabErrorMessage.OnError);
        }
        
        private void OnDataSend(SetTitleDataResult result)
        {
            Debug.Log("Success");
        }

     
    }
}