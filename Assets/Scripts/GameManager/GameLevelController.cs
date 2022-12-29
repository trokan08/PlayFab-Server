using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GamePlay.EntityClass;
using GamePlay.Enums;
using PlayFab.ClientServices.GameAnalytics;
using PlayFab.ClientServices.GamePlayConfigs.GameLevel;
using PlayFab.ClientServices.PlayerNameInformation;
using PlayFab.ConfigDatas;
using PoolSystem;
using ScriptableObjects.ConfigData;
using UnityEngine;
using UnityEngine.SceneManagement;
using LevelVO = PlayFab.ClientServices.GameAnalytics.LevelVO;

namespace GameManager
{
    public class GameLevelController : MonoBehaviour
    {
        private Dictionary<LevelName, LevelName> _levelMapping = new Dictionary<LevelName, LevelName>();
        private LevelName _lastLevel;
        private Action _UIClose;
        private Timer _timer = new Timer();
        private void OnEnable()
        {
            Debug.Log("Load");
            GameActions.Instance.StartGame += SetLevels;
            GameActions.Instance.SaveGame += SetNextLevel;
            GameActions.Instance.NextLevel += InstantiateLevel;
            GameActions.Instance.PlayedLevel += GetLevelPlayTime;
        }

        private void OnDisable()
        {
            Debug.Log("UnLoad");
            GameActions.Instance.StartGame -= SetLevels;
            GameActions.Instance.SaveGame -= SetNextLevel;
            GameActions.Instance.NextLevel -= InstantiateLevel;
            GameActions.Instance.PlayedLevel -= GetLevelPlayTime;
        }

        private void SetLevels(Action registerUI)
        {
            _UIClose = registerUI;
            GetLevelsService getLevelsService = new GetLevelsService();
            getLevelsService.GetLevels(SetLevelMapping);
        }

        private void SetLevelMapping(ScriptableObjects.ConfigData.LevelVO levels)
        {
            Debug.Log("Get Levelssssss");
            LevelMapping[] levelMappings = levels.Levels;
            foreach (var level in levelMappings)
            {
                _levelMapping.Add(level.Level,level.NextLevel);
            }
            
            InstantiateLevel();
            
        }
        
        
        

        public  void InstantiateLevel()
        {
            GetPlayerLevelService getPlayerLevelService = new GetPlayerLevelService();
            getPlayerLevelService.GetPlayerLevel(OnLevelInstantiate);
        }
        
        public  void InstantiateLevel(Action registerUI)
        {
            _UIClose = registerUI;
            GetPlayerLevelService getPlayerLevelService = new GetPlayerLevelService();
            getPlayerLevelService.GetPlayerLevel(OnLevelInstantiate);
        }

        private void OnLevelInstantiate(PlayerLevel playerLevel)
        {
            if (playerLevel.IsFirstTime)
            {
                _lastLevel = _levelMapping.Values.ToList()[0];
            }
            else
            {
                _lastLevel = playerLevel.CurrentLevel;
            }
            
            StartCoroutine(LoadScene(_lastLevel.ToString()));

            
        }
        
        
        private IEnumerator LoadScene(string levelName)
        {
           

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            

            GetLevelInformationService getLevelInformationService = new GetLevelInformationService();
            getLevelInformationService.InstantiateLevel(levelName,OnInstantiateLevelObjects);
            _UIClose.Invoke();
            GameActions.Instance.UIReload.Invoke();
            _timer.StartTimer(this);

        }

        private void OnInstantiateLevelObjects(ObstacleVO[] obstacles)
        {
            GameObject obstaclesParent = GameObject.Find("ObstaclesParent");

            foreach (var obstacle in obstacles)
            {
                Vector3 position = new Vector3(obstacle.Positions[0], obstacle.Positions[1], obstacle.Positions[2]);
                GameObject gameObject =
                    ObjectsPool.Instance.GetFromPool(ObstacleTypeConverter.ObstacleTypeMapping[obstacle.ObstacleName]);
                gameObject.transform.parent = obstaclesParent.transform;
                gameObject.transform.position = position;
            }
            

        }

        private void UnloadLevel()
        {
            SceneManager.UnloadSceneAsync(_lastLevel.ToString());
        }

        private void SetNextLevel()
        {
            Debug.Log("Next Level");
            ObjectsPool.Instance.DeActivateObjects();
            UnloadLevel();
            _lastLevel = _levelMapping[_lastLevel];
            SendPlayerLevelService sendPlayerLevelService = new SendPlayerLevelService();
            sendPlayerLevelService.SendPlayerData(_lastLevel);
        }

        private LevelVO GetLevelPlayTime()
        {
            LevelVO level = _timer.GetTime(this);
            level.LevelName = _lastLevel.ToString();
            return level;

        }

        
    }
    
}