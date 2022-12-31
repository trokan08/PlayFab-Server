using System;
using GameManager;
using Network.PlayFab.Friends;
using UnityEngine;
using UnityEngine.UI;

namespace KnoxGameStudios
{
    public class UIAddFriend : MonoBehaviour
    {
        [SerializeField] private string displayName;
        [SerializeField] private Button _addButton;

        private void OnEnable()
        {
            _addButton.onClick.AddListener( AddFriend);
            AddRemoveFriendService.Instance().GetPlayfabFriends();

        }

        private void OnDisable()
        {
            _addButton.onClick.RemoveListener( AddFriend);
        }

        public void SetDisplayName(string text)
        {
            displayName = text;
        }
        
        
        public void AddFriend()
        {
            if (!string.IsNullOrEmpty(displayName))
            {
                Debug.Log($"UI Add Friend Clicked: {displayName}");

                AddRemoveFriendService.Instance().HandleAddPlayfabFriend(displayName);
            } 
            
        }
    }
}