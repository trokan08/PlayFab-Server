using GameManager;
using GamePlay.Enums;
using UnityEngine;

namespace GamePlay.Trigger
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private ObstacleType _obstacleType;

        public ObstacleType ObstacleType
        {
            get => _obstacleType;
        }

       
    }
}