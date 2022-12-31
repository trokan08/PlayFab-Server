using System;
using System.Collections.Generic;
using System.Linq;
using GameManager;
using Network.Photon;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.ConfigDatas;
using UnityEngine;

namespace Network.PlayFab.Friends
{
    public class AddRemoveFriendService 
    {
       private List<FriendInfo> friends = new List<FriendInfo>();
       static AddRemoveFriendService instance;

       // Constructor is 'protected'
       protected AddRemoveFriendService()
       {
       }
       public static AddRemoveFriendService Instance()
       {
           // Uses lazy initialization.
           // Note: this is not thread safe.
           if (instance == null)
           {
               instance = new AddRemoveFriendService();
           }
           return instance;
       }
       
        #region Private Methods
        public void HandleAddPlayfabFriend(string name)
        {
            Debug.Log($"Playfab add friend request for {name}");
            var request = new AddFriendRequest { FriendTitleDisplayName = name };
            PlayFabClientAPI.AddFriend(request, OnFriendAddedSuccess, PlayFabErrorMessage.OnError);
        }
        public void HandleRemoveFriend(string name)
        {
            string id = friends.FirstOrDefault(f => f.TitleDisplayName == name).FriendPlayFabId;
            Debug.Log($"Playfab remove friend {name} with id {id}");
            var request = new RemoveFriendRequest { FriendPlayFabId = id };
            PlayFabClientAPI.RemoveFriend(request, OnFriendRemoveSuccess, PlayFabErrorMessage.OnError);
        }

        

        public void GetPlayfabFriends()
        {
            Debug.Log("Playfab get friend list request");
            var request = new GetFriendsListRequest { IncludeSteamFriends = false, IncludeFacebookFriends = false, XboxToken = null };
            PlayFabClientAPI.GetFriendsList(request, OnFriendsListSuccess, PlayFabErrorMessage.OnError);
        }
        #endregion

        #region Playfab Call backs
        private void OnFriendAddedSuccess(AddFriendResult result)
        {
            Debug.Log("Playfab add friend success getting updated friend list");
            GetPlayfabFriends();
        }

        private void OnFriendsListSuccess(GetFriendsListResult result)
        {
            Debug.Log($"Playfab get friend list success: {result.Friends.Count}");
            friends = result.Friends;
            PhotonFriendUpdate.Instance.HandleFriendsUpdated(friends);
        }

        private void OnFriendRemoveSuccess(RemoveFriendResult result)
        {
            Debug.Log($"Playfab remove friend success");
            GetPlayfabFriends();
        }

       
        #endregion
    }
}