using UnityEngine;
using System;
using Photon.Chat;
using Photon.Pun;
using ExitGames.Client.Photon;
using System.Collections.Generic;
using GameManager;
using Photon.Chat.Demo;
using PlayFab.ClientModels;

namespace KnoxGameStudios
{
    public class PhotonChatController : MonoBehaviour, IChatClientListener
    {
        private string nickName;
        private ChatClient chatClient;
        private bool _update;
            public static Action<string, string> OnRoomInvite = delegate { };
        public static Action<ChatClient> OnChatConnected = delegate { };

        #region Unity Methods

      

        private void Start()
        {
            GameActions.Instance.SetUsername += ConnectChat;

        }

        private void OnDisable()
        {
            GameActions.Instance.SetUsername -= ConnectChat;

        }


        public void ConnectChat(string userName)
        {
            nickName = userName;
            chatClient = new ChatClient(this);
            ConnectoToPhotonChat();
            _update = true;
        }

      
        private void Update()
        {
            if (_update )
            {
                chatClient.Service();

            }
        }

        #endregion

        #region  Private Methods

        private void ConnectoToPhotonChat()
        {
            Debug.Log("Connecting to Photon Chat");
            chatClient.AuthValues = new AuthenticationValues(nickName);
            ChatAppSettings chatSettings = PhotonNetwork.PhotonServerSettings.AppSettings.GetChatSettings();
            chatClient.ConnectUsingSettings(chatSettings);
        }

        #endregion

        #region  Public Methods

        public void HandleFriendInvite(string recipient)
        {
            chatClient.SendPrivateMessage(recipient, PhotonNetwork.CurrentRoom.Name);
        }

        #endregion

        #region Photon Chat Callbacks

        public void DebugReturn(DebugLevel level, string message)
        {
            Debug.Log($"Photon Chat DebugReturn: {message}");
        }

        public void OnDisconnected()
        {
            Debug.Log("You have disconnected from the Photon Chat");
            chatClient.SetOnlineStatus(ChatUserStatus.Offline);
        }

        public void OnConnected()
        {
            Debug.Log("You have connected to the Photon Chat");
            OnChatConnected?.Invoke(chatClient);
            chatClient.SetOnlineStatus(ChatUserStatus.Online);
        }

        public void OnChatStateChange(ChatState state)
        {
            Debug.Log($"Photon Chat OnChatStateChange: {state.ToString()}");
        }

        public void OnGetMessages(string channelName, string[] senders, object[] messages)
        {
            
        }

        public void OnPrivateMessage(string sender, object message, string channelName)
        {
            
        }

        public void OnSubscribed(string[] channels, bool[] results)
        {
           
        }

        public void OnUnsubscribed(string[] channels)
        {
            
        }

        public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
        {
            
        }

        public void OnUserSubscribed(string channel, string user)
        {
            Debug.Log($"Photon Chat OnUserSubscribed: {channel} {user}");
        }

        public void OnUserUnsubscribed(string channel, string user)
        {
            Debug.Log($"Photon Chat OnUserUnsubscribed: {channel} {user}");
        }
        #endregion
    }
}