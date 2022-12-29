using System;
using GameManager;
using PlayFab.ClientServices.Username;
using PlayFab.ConfigDatas;
using TMPro;
using UnityEngine;

namespace GamePlay.Screen.UsernameUI
{
    public class UsernameUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _userName;
        [SerializeField] private GameObject _parent;

        private void OnEnable()
        {
          Invoke(nameof(AddListener),0.1f);
        }

        private void OnDisable()
        {
            GameActions.Instance.SetUsername -= SetText;
            GameActions.Instance.GameSucces -= () =>
            {
                DeActiveObject(false);
            };
            GameActions.Instance.GetUsername -= GetUsername;
            GameActions.Instance.UIReload -= () =>
            {
                DeActiveObject(true);
            };
        }

        private void AddListener()
        {
            GameActions.Instance.SetUsername += SetText;
            GameActions.Instance.GameSucces += () =>
            {
                DeActiveObject(false);
            };
            GameActions.Instance.GetUsername += GetUsername;
            GameActions.Instance.UIReload += () =>
            {
                DeActiveObject(true);
            };
        }

        private void SetText(string text)
        {
            _parent.SetActive(true);
            _userName.text = text;
            
        }

        private void DeActiveObject(bool bo)
        {
            _parent.SetActive(bo);
        }

        private void GetUsername(string email)
        {
            GetUsernameService getUsernameService = new GetUsernameService();
            getUsernameService.GetUserName(email);
        }
        
        
    }
}