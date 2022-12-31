using System;
using System.Collections.Generic;
using System.Linq;
using GameManager;
using Photon.Pun;
using PlayFab.ClientModels;
using UnityEngine;
using PlayfabFriendInfo = PlayFab.ClientModels.FriendInfo;
using PhotonFriendInfo = Photon.Realtime.FriendInfo;

namespace Network.Photon
{
    public class PhotonFriendUpdate : MonoBehaviourPunCallbacks
    {
        private static PhotonFriendUpdate instance;

        public static PhotonFriendUpdate Instance
        {
            get => instance;
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public void HandleFriendsUpdated(List<PlayfabFriendInfo> friends)
        {
            if (friends.Count != 0)
            {
                string[] friendDisplayNames = friends.Select(f => f.TitleDisplayName).ToArray();
                PhotonNetwork.FindFriends(friendDisplayNames);
            }
            else
            {
                GameActions.Instance.OnFriendListUpdated.Invoke(null);

            }
         
        }
        
     
        
        public override void OnFriendListUpdate(List<PhotonFriendInfo> friendList)
        {
            GameActions.Instance.OnFriendListUpdated.Invoke(friendList);
        }
    }
}