using GameManager;
using PlayFab.ClientModels;
using PlayFab.ConfigDatas;

namespace PlayFab.ClientServices.Username
{
    public class GetUsernameService
    {
        public void GetUserName(string playFabId)
        {
            var request = new GetPlayerProfileRequest()
                {
                    PlayFabId = playFabId,
                    ProfileConstraints = new PlayerProfileViewConstraints
                    {
                        ShowDisplayName = true,
                    }
                }
                ;
            
            PlayFabClientAPI.GetPlayerProfile(request,OnAccountInfo,PlayFabErrorMessage.OnError);
        }

        private void OnAccountInfo(GetPlayerProfileResult result)
        {
            //Debug.Log(result.PlayerProfile.DisplayName);
            GameActions.Instance.SetUsername(result.PlayerProfile.DisplayName);
        }
        
    }
}