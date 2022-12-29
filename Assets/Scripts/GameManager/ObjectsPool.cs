using System;
using System.Collections.Generic;
using GamePlay.Enums;
using UnityEngine;

namespace PoolSystem
{
    public class ObjectsPool : MonoBehaviour
    {
        //Singleton
        public static ObjectsPool Instance => _instance;
        private static ObjectsPool _instance;
        
        [Serializable]
        public class PoolItem
        {
            public ObstacleType ObstacleType;
            public GameObject gameObject;
            public int count;
        }
        
        public List<PoolItem> poolItems = new List<PoolItem>();
        public Dictionary<ObstacleType, List<GameObject>> poolDictionary = new Dictionary<ObstacleType, List<GameObject>>();

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
            
            //if amount variables of items are higher than 0, instantiate them
            foreach (var item in poolItems)
            {
                if (item.count > 0)
                {
                    for (int i = 0; i < item.count; i++)
                    {
                        var obj = Instantiate(item.gameObject, transform);
                        DontDestroyOnLoad(obj.gameObject);

                        obj.SetActive(false);
                        AddToPool(item.ObstacleType, obj);
                    }
                }
            }
        }

        /*
        private void Start()
        {

        }
        */
        
        
        
        
        public GameObject GetFromPool(ObstacleType obstacleType)
        {
            //if pooled item exists, return it
            if (poolDictionary.ContainsKey(obstacleType))
            {
                foreach (var item in poolDictionary[obstacleType])
                {
                    if (!item.activeInHierarchy)
                    {
                        // item.SetActive(true);
                        return item;
                    }
                }
            }
            //if not instantiate a new one and add it to the pool
            var obj = Instantiate(GetObstacle(obstacleType), transform);
            DontDestroyOnLoad(obj.gameObject);

            // obj.SetActive(true);
            AddToPool(obstacleType, obj, 1);
            return obj;
        }

        
        public void AddToPool(ObstacleType obstacleType, GameObject obj, int count = 1)
        {
            
            if (!poolDictionary.ContainsKey(obstacleType))
            {
                //add to pool dictionary with count

                for (int i = 0; i < count; i++)
                {
                    poolDictionary.Add(obstacleType, new List<GameObject>());
                }
            }
            poolDictionary[obstacleType].Add(obj);
        }

        private GameObject GetObstacle(ObstacleType obstacleType)
        {
            foreach (var vaPoolItem in poolItems)
            {
                if (vaPoolItem.ObstacleType == obstacleType)
                {
                    return vaPoolItem.gameObject;
                }
            }

            return null;
        }

        public void DeActivateObjects()
        {
            foreach (ObstacleType obstacleType in poolDictionary.Keys)
            {
                List<GameObject> gameObjects = poolDictionary[obstacleType];
                int count = gameObjects.Count;
                for (int i = count-1; i >= 0; i--)
                {
                    gameObjects.Remove(gameObjects[i]);

                }
                
               
            }
        }
    }
}