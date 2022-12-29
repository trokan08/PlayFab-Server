using PlayFab.ClientModels;
using PlayFab.ConfigDatas;

namespace PlayFab.ClientServices.CloudScripts
{
    public class SendPlayerInformations
    {
        public void StartCloudUpdatePlayerStats()
        {
            PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            {
                FunctionName = "UpdatePlayerStats", // Arbitrary function name (must exist in your uploaded cloud.js file)
                FunctionParameter = new { Level = "playerLevel", highScore ="playerHighScore"}, // The parameter provided to your function
                GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
            }, OnCloudUpdateStats, PlayFabErrorMessage.OnError);
        }

        private void OnCloudUpdateStats(ExecuteCloudScriptResult result)
        {
            
        }
    }
}