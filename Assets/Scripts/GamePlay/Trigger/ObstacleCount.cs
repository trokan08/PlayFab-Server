using System;
using GameManager;
using UnityEngine;

namespace GamePlay.Trigger
{
    public class ObstacleCount : MonoBehaviour
    {
        private int _obstacleCollideCount;

        private void OnEnable()
        {
            _obstacleCollideCount = 0;
            GameActions.Instance.ObstacleCollidedCount += GetObstacleCollideCount;
        }

        private void OnDisable()
        {
            GameActions.Instance.ObstacleCollidedCount -= GetObstacleCollideCount;
        }


        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collisiiooon");
            _obstacleCollideCount = +1;
        }

        public int GetObstacleCollideCount()
        {
            return _obstacleCollideCount;
        }
    }
}