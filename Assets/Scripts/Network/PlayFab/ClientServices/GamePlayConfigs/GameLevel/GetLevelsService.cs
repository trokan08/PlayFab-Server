using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PlayFab.ClientModels;
using PlayFab.ConfigDatas;
using ScriptableObjects.ConfigData;

namespace PlayFab.ClientServices.GamePlayConfigs.GameLevel
{
    public class GetLevelsService
    {
        private Action<LevelVO> _setLevels;
        
        public void GetLevels(Action<LevelVO> setLevels)
        {
            _setLevels = setLevels;
            List<string> levels = new List<string>(){"Levels"};

            var request = new GetTitleDataRequest
            {
                Keys = levels
            };
            PlayFabClientAPI.GetTitleData(request,OnLevelInstantiateData,PlayFabErrorMessage.OnError);

        }
        
        private void OnLevelInstantiateData(GetTitleDataResult result)
        {
            if (result != null )
            {
                //Debug.Log("Succes");
                LevelVO levels = JsonConvert.DeserializeObject<LevelVO>(result.Data["Levels"]);
                _setLevels.Invoke(levels);
                
            }
        }
    }
}