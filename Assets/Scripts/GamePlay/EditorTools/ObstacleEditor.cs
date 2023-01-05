
using GamePlay.Trigger;
using UnityEngine;
using Newtonsoft.Json;

using PlayFab.ServerServices.GameLevel;
using ScriptableObjects.ConfigData;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using UnityEditor;
using PlayFabError = PlayFab.PlayFabError;

namespace GamePlay.EntityClass
{
    public class ObstacleEditor : MonoBehaviour
    {

#if UNITY_EDITOR
        
        
        [Button]
        public void SaveObstacles()
        {
            LevelDesignVO levelDesign = new LevelDesignVO();
            levelDesign.LevelName = SceneManager.GetActiveScene().name;
            
            
            Obstacle[] obstacleTypes = FindObjectsOfType<Obstacle>();
            ObstacleVO[] obstacles = new ObstacleVO[obstacleTypes.Length];
            int count = obstacleTypes.Length;
            for (int i = 0; i < count; i++)
            {
                Obstacle tmpObstacle = obstacleTypes[i];
                ObstacleVO obstacle = new ObstacleVO();

                float[] positions = new float[3];
                Vector3 position = tmpObstacle.transform.position;
                positions[0] = position.x;
                positions[1] = position.y;
                positions[2] = position.z;

                obstacle.Positions = positions;
                obstacle.ObstacleName = tmpObstacle.ObstacleType.ToString();

                obstacles[i] = obstacle;

            }

            levelDesign.Obstacles = obstacles;
            Debug.Log(JsonConvert.SerializeObject(levelDesign));

            SendLevelDesignService sendLevelDesignService = new SendLevelDesignService();
            sendLevelDesignService.SendLevelInformation(levelDesign);


        }

       
        
        
#endif
        
        
        
    }
}