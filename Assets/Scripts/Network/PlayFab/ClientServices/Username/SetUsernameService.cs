using PlayFab.ClientModels;
using PlayFab.ConfigDatas;
using UnityEngine;

namespace PlayFab.ClientServices.Username
{
    public class SetUsernameService
    {
        public void SetUserName(string username)
        {
            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = username,

            };
            PlayFabClientAPI.UpdateUserTitleDisplayName(request,OnDisplayUsername,PlayFabErrorMessage.OnError);

        }

        private void OnDisplayUsername(UpdateUserTitleDisplayNameResult result)
        {
            Debug.Log(result.DisplayName);
        }
    }
}