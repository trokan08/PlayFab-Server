using System.Collections.Generic;
using GamePlay.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
namespace ScriptableObjects.ConfigData
{
    [System.Serializable]
    public class LevelVO
    {
        public LevelMapping[] Levels;

       
    }
    [System.Serializable]

    public class LevelMapping
    {
        [JsonConverter(typeof(StringEnumConverter))] public LevelName Level;
        [JsonConverter(typeof(StringEnumConverter))] public LevelName NextLevel;
    }
}