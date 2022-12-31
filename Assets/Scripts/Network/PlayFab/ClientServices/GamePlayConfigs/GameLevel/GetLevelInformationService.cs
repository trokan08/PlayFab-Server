using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PlayFab.ClientModels;
using PlayFab.ConfigDatas;
using ScriptableObjects.ConfigData;

namespace PlayFab.ClientServices.GamePlayConfigs.GameLevel
{
    public class GetLevelInformationService
    {
        private string _levelName;

        private Action<ObstacleVO[]> _createObstacles;
        
        public void InstantiateLevel(string levelName,Action<ObstacleVO[]> createObstacles)
        {
            List<string> levels = new List<string>(){levelName};
            var request = new GetTitleDataRequest
            {
                Keys = levels
            };
            _levelName = levelName;
            _createObstacles = createObstacles;
            PlayFabClientAPI.GetTitleData(request,OnLevelInstantiateData,PlayFabErrorMessage.OnError);

        }
        
        private void OnLevelInstantiateData(GetTitleDataResult result)
        {
            if (result != null && result.Data.ContainsKey(_levelName))
            {
                //Debug.Log("Succes");
                LevelDesignVO level = JsonConvert.DeserializeObject<LevelDesignVO>(result.Data[_levelName]);
                _createObstacles.Invoke(level.Obstacles);
            }
        }
        
       
    }
}