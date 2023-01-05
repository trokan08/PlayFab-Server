using System.Security.Cryptography;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System;
using System.Text;
using GameManager;
using PlayFab.ClientServices.Username;
using PlayFab.ConfigDatas;

namespace Login
{
    public partial class RegisterLogin : MonoBehaviour
    {

        private string _userEmail;
        private string _userPassword;
        private string _username;

        private void Register()
        {
            if(!IsPasswordValid()) return;
            
            var request = new RegisterPlayFabUserRequest
            {
                Email = _emailInputRegister.text,
                Password = Encrypt(_passwordInputRegister.text),
                RequireBothUsernameAndEmail = false
            };
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
            
        }

        private void Login()
        {
            var request = new LoginWithEmailAddressRequest
            {
                Email = _emailInputLogin.text,
                Password = Encrypt(_passwordInputLogin.text)
            };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
        }

        private void ResetPassword()
        {
            if (_emailInputLogin == null)
            {
                _message.text = "Email can not be Empty";
            }
            
            var request = new SendAccountRecoveryEmailRequest()
            {
                Email = _emailInputLogin.text,
                TitleId = "5993B"
            };
            PlayFabClientAPI.SendAccountRecoveryEmail(request,OnResetSuccess, OnError);
        }

        private void OnResetSuccess(SendAccountRecoveryEmailResult obj)
        {
            _message.text = "Reset password mail is send";

        }

        private void OnLoginSuccess(LoginResult result)
        {
            _message.text = "Logged in!";
            GameActions.Instance.StartGame.Invoke(DeActiveObject);
            GameStatistics.PlayFabID = result.PlayFabId;
//            Debug.Log(result.InfoResultPayload.PlayerProfile.PlayerId);
            GameActions.Instance.GetUsername.Invoke(result.PlayFabId);
            GameActions.Instance.MatchMaking?.Invoke(result.EntityToken.Entity.Id);
            // GameActions.instance.SetUsername(result.InfoResultPayload.PlayerProfile.PlayerId);



        }

        private string Encrypt(string pass)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider encCryptoServiceProvider =
                new MD5CryptoServiceProvider();
            UTF8Encoding utf8 = new UTF8Encoding();

            byte[] bs = utf8.GetBytes(pass);
            System.Text.StringBuilder  stringBuilder= new System.Text.StringBuilder();

            foreach (var b in bs)
            {
                stringBuilder.Append(b.ToString("x2").ToLower());
            }

            return stringBuilder.ToString();

        }

        private bool IsPasswordValid()
        {
            if (_passwordInputRegister.text.Length < 6)
            {
                _message.text = "Password too short";
                return false;
            }

            return true;
        }

        private void OnRegisterSuccess(RegisterPlayFabUserResult result)
        {
            SetUsernameService usernameService = new SetUsernameService();
            usernameService.SetUserName(_usernameInputRegister.text);
            _message.text = "Registered and logged in!";
            DeActiveObject();
            GameActions.Instance.StartGame.Invoke(DeActiveObject);
            GameActions.Instance.SetUsername.Invoke(_usernameInputRegister.text);
        }

        private void OnError(PlayFabError error)
        {
            _message.text = error.ErrorMessage;
        }

        
    }
}
