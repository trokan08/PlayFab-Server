using GameManager;
using GamePlay.Enums;
using UnityEngine;

namespace GamePlay.Trigger
{
    public class Obstacle : MonoBehaviour, ITrigger
    {
        [SerializeField] private ObstacleType _obstacleType;

        public ObstacleType ObstacleType
        {
            get => _obstacleType;
        }

        public void Action(GameObject gameObject)
        {
            GameActions.Instance.CoinCollected.Invoke(-1);
        }
    }
}