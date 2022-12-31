using System;
using System.Collections.Generic;
using GamePlay.Screen.LeaderBoard;
using PlayFab.ClientModels;
using PlayFab.ConfigDatas;

namespace PlayFab.ClientServices.Score
{
    public class GetLeaderBoardService
    {
        private Action<List<Scores>> _scores;
        public void GetLeaderBoard(Action<List<Scores>> scores)
        {
            _scores = scores;
            var request = new GetLeaderboardRequest
            {
                StatisticName = "Score",
                StartPosition = 0,
                MaxResultsCount = 5,
            };
            PlayFabClientAPI.GetLeaderboard(request,OnLeaderboardGet,PlayFabErrorMessage.OnError);
        }

        private void OnLeaderboardGet(GetLeaderboardResult result)
        {
            List<Scores> scoresList = new List<Scores>();

            foreach (var item in result.Leaderboard)
            {
                Scores scores = new Scores();
                scores.Position = item.Position + 1;
                scores.Score = item.StatValue;
                scores.ID = item.DisplayName;
                scoresList.Add(scores);
            }
            
            _scores.Invoke(scoresList);
        }

      
        
        
    }
    
    
}