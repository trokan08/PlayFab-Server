using System;
using System.Collections.Generic;
using System.Linq;
using GameManager;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;

namespace GamePlay.Screen.Friends
{
    public class DisplayFriends : MonoBehaviour
    {
        [SerializeField] private Transform friendContainer;
        [SerializeField] private FriendUI uiFriendPrefab;

        private void OnEnable()
        {
            GameActions.Instance.OnFriendListUpdated += HandleDisplayFriends;
        }

        

        private void OnDisable()
        {
            GameActions.Instance.OnFriendListUpdated -= HandleDisplayFriends;
        }


        private void HandleDisplayFriends(List<FriendInfo> friends)
        {
            
            
            Debug.Log("UI remove prior friends displayed");
            foreach (Transform child in friendContainer)
            {
                Destroy(child.gameObject);
            }

            if (friends != null)
            {
                foreach (FriendInfo friend in friends)
                {
                    FriendUI uifriend = Instantiate(uiFriendPrefab, friendContainer);
                    uifriend.Initialize(friend);
                }
            }
           
        }
       
        
    }
}