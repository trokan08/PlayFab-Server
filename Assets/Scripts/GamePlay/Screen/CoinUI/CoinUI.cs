using System;
using GameManager;
using PlayFab.ClientServices.Score;
using PlayFab.ConfigDatas;
using TMPro;
using UnityEngine;

namespace GamePlay.Screen.CoinUI
{
    public class CoinUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinText;
        [SerializeField] private GameObject _parent;
        private int coinCount = 0;
        private void OnEnable()
        {
            _coinText.text = "0";
            GameActions.Instance.CoinCollected += CoinCollected;
            GameActions.Instance.SaveGame += SavePlayerScore;
            GameActions.Instance.GameSucces += SaveCoin;
            GameActions.Instance.CoinCount += GetCoinCount;



        }

       

        private void OnDisable()
        {
            GameActions.Instance.CoinCollected -= CoinCollected;
            GameActions.Instance.SaveGame -= SavePlayerScore;
            GameActions.Instance.CoinCount -= GetCoinCount;
            GameActions.Instance.GameSucces -= SaveCoin;
           

        }

        private void CoinCollected()
        {
            coinCount += 1;

            _coinText.text = coinCount.ToString();
        }

        private void SavePlayerScore()
        {
            SendPlayerScoreService scoreService = new SendPlayerScoreService();
            scoreService.SendScore(coinCount);
        }

        private void DeActiveObject(bool bo)
        {

            gameObject.SetActive(bo);
        }

        private void SaveCoin()
        {
            GameActions.Instance.SetGameFinishUI.Invoke(coinCount);

        }

        private int GetCoinCount()
        {
            return coinCount;
        }
        
    }
}