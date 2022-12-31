using UnityEngine;

namespace PlayFab.ConfigDatas
{
    public static class PlayFabErrorMessage
    {
        public static void OnError(PlayFabError playFabError)
        {
            Debug.Log(playFabError.Error);
        }
    }
}