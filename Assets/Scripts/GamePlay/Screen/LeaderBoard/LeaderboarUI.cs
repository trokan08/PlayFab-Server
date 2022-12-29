using System;
using System.Collections.Generic;
using GameManager;
using NaughtyAttributes;
using PlayFab.ClientServices.Score;
using PlayFab.ConfigDatas;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace GamePlay.Screen.LeaderBoard
{
    public class LeaderboarUI : MonoBehaviour
    {
       [SerializeField] private List<ScoreContainer> _scoreContainers = new List<ScoreContainer>();
       [SerializeField] private GameObject _uıParent;
       private Action _finishUI;
       private void OnEnable()
       {
           Invoke(nameof(AddListener),0.1f);
       }

       private void AddListener()
       {
           GameActions.Instance.OpenLeaderBoard += SetScores;

       }

       private void OnDisable()
       {
           GameActions.Instance.OpenLeaderBoard -= SetScores;
       }

       public void SetScores(Action finishUI)
       {
           Debug.Log("Set Scores");
           _finishUI = finishUI;
           GetLeaderBoardService getLeaderBoardService = new GetLeaderBoardService();
           getLeaderBoardService.GetLeaderBoard(SetTexts);
       }

       private void SetTexts(List<Scores> scoresList)
       {
           _finishUI.Invoke();
           _uıParent.SetActive(true);

           int count = scoresList.Count;
           
           for (int i = 0; i < count; i++)
           {
               ScoreContainer  scoreContainer = _scoreContainers[i];
               Scores scores = scoresList[i];
               scoreContainer.ID.text = scores.ID;
               scoreContainer.Score.text = scores.Score.ToString();
               scoreContainer.Position.text = scores.Position.ToString();
           }
       }
       
       
    }

    [System.Serializable]
    public class ScoreContainer
    {
        public TextMeshProUGUI Position;
        public TextMeshProUGUI ID;
        public TextMeshProUGUI Score;
        
    }

    public class Scores
    {
        public int Position;
        public int Score;
        public string ID;
    }
}