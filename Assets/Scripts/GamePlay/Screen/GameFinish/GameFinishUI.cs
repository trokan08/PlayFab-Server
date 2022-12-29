using System;
using GameManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Screen.GameFinish
{
    public class GameFinishUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private Button _leaderBoard;
        [SerializeField] private Button _nextLevel;
        [SerializeField] private GameObject _parent;

        private void OnEnable()
        {
            _leaderBoard.onClick.AddListener(OpenLeaderBoard);
            _nextLevel.onClick.AddListener(NextLevel);
            Invoke(nameof(AddListener),0.1f);

        }

        private void AddListener()
        {
            GameActions.Instance.SetGameFinishUI += SetScore;

        }

        private void OnDisable()
        {
            
            _leaderBoard.onClick.RemoveListener(OpenLeaderBoard);
            _nextLevel.onClick.RemoveListener(NextLevel);
            GameActions.Instance.SetGameFinishUI -= SetScore;
        }

        private void SetScore(int score)
        {
            _score.text ="Score: " +  score.ToString();
            _parent.SetActive(true);
        }


        private void DeActiveObject()
        {
            _parent.SetActive(false);
        }

        private void OpenLeaderBoard()
        {
            GameActions.Instance.OpenLeaderBoard.Invoke(DeActiveObject);
        }

        private void NextLevel()
        {
            GameActions.Instance.NextLevel.Invoke(DeActiveObject);
        }

    }
}