using GameManager;
using Network.PlayFab.Friends;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Screen.Friends
{
    public class FriendUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text friendNameText;
        private FriendInfo _friendInfo;
        [SerializeField] private Button _removeFriend;
        [SerializeField] private Button _inviteFriend;

        private void OnEnable()
        {
            _removeFriend.onClick.AddListener(RemoveFriend);
            _inviteFriend.onClick.AddListener(InviteFriend);
        }

        private void OnDisable()
        {
            _removeFriend.onClick.RemoveListener(RemoveFriend);
            _inviteFriend.onClick.RemoveListener(InviteFriend);

        }

        public void Initialize(FriendInfo friendInfo)
        {
            _friendInfo = friendInfo;
            friendNameText.SetText(friendInfo.UserId);
        }

        public void RemoveFriend()
        {
            AddRemoveFriendService.Instance().HandleRemoveFriend(_friendInfo.UserId);
        }
        
        public void InviteFriend()
        {
            GameActions.Instance.InviteFriend.Invoke(_friendInfo);
        }
    }
}