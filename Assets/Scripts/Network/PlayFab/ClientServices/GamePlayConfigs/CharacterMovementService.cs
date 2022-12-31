using System;
using PlayFab.ClientModels;
using PlayFab.ConfigDatas;
using ScriptableObjects.ConfigData;
using UnityEngine;

namespace PlayFab.ClientServices.GamePlayConfigs
{
    public class CharacterMovementService
    {
        private Action<CD_MovementSpeed> _setSpeed;
        public void GetCharacterMovement(Action<CD_MovementSpeed> setSpeed)
        {
            _setSpeed = setSpeed;
            PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(),OnMovementDataReceived,PlayFabErrorMessage.OnError);
        }
        
        private void OnMovementDataReceived(GetTitleDataResult result)
        {
            if (result != null && result.Data.ContainsKey("Speed") && result.Data.ContainsKey("TurnSpeed"))
            {
                CD_MovementSpeed cdMovementSpeed =  Resources.Load<CD_MovementSpeed>("Data/Config/Movement");
                cdMovementSpeed.CharacterMovementProperties.MoveSpeed = Convert.ToInt32(result.Data["Speed"]);
                cdMovementSpeed.CharacterMovementProperties.TurnSpeed = Convert.ToInt32(result.Data["TurnSpeed"]);
                _setSpeed.Invoke(cdMovementSpeed);
            }
        }

       
    }
}