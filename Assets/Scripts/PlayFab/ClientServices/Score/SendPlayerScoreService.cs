using System.Collections.Generic;
using PlayFab.ClientModels;
using PlayFab.ConfigDatas;

namespace PlayFab.ClientServices.Score
{
    public class SendPlayerScoreService
    {
        public void SendScore(int score)
        {
            
            var request = new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>
                {
                    new StatisticUpdate
                    {
                        StatisticName = "Score",
                        Value = score
                        
                    }
                }
            };
            PlayFabClientAPI.UpdatePlayerStatistics(request,OnLeaderboarUpdate,PlayFabErrorMessage.OnError);
        }

        private void OnLeaderboarUpdate(UpdatePlayerStatisticsResult result)
        {
            
        }

       
    }
}