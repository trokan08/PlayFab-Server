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

        private void OnEnable()
        {
            _removeFriend.onClick.AddListener(RemoveFriend);
        }

        private void OnDisable()
        {
            _removeFriend.onClick.RemoveListener(RemoveFriend);
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
    }
}