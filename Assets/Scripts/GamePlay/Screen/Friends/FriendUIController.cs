using System;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Screen.Friends
{
    public class FriendUIController : MonoBehaviour
    {
        [SerializeField] private Button _friend;
        [SerializeField] private Button _party;
        [SerializeField] private  GameObject _friendUI;
        [SerializeField] private  GameObject _partyUI;
        private bool _friendBool = false;
        private bool _partydBool = false;
        private void OnEnable()
        {
            _friendBool = false;
            _partydBool = false;
            _friendUI.SetActive(false);
            _partyUI.SetActive(false);
            _friend.onClick.AddListener(ActivateFrienddUI);
            _party.onClick.AddListener(ActivatePartydUI);
        }

        private void OnDisable()
        {
            _friend.onClick.RemoveListener(ActivateFrienddUI);
            _party.onClick.RemoveListener(ActivatePartydUI);
        }

        private void ActivateFrienddUI()
        {
            _partyUI.SetActive(false);
            _friendUI.SetActive(!_friendBool);
            _friendBool = !_friendBool;
            _partydBool = false;

        }
        
        private void ActivatePartydUI()
        {
            _friendUI.SetActive(false);
            _partyUI.SetActive(!_partydBool);
            _partydBool = !_partydBool;
            _friendBool = false;

        }
        
        
    }
}