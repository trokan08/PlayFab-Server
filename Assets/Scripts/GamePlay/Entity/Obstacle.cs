using GamePlay.Enums;
using UnityEngine;

namespace GamePlay.EntityClass
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