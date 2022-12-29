using System.Collections.Generic;
using PlayFab.ServerServices.GameLevel;
using ScriptableObjects.ConfigData;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay.EntityClass
{
    [CreateAssetMenu(menuName = "Data/Config/LevelEditor", order = 1)]

    public class LevelEditor : SerializedScriptableObject
    {
        public LevelVO Levels;

        [Button]
        public void SetLevel()
        {
            SendLevelsService sendLevelsService = new SendLevelsService();
            sendLevelsService.SendLevels(Levels);
        }
        
        
    }
}