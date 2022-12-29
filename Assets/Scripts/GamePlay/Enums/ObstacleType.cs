using System.Collections.Generic;

namespace GamePlay.Enums
{
    public enum ObstacleType
    {
        Cube,
        Sphere,
        Cylinder
    }

    public static class ObstacleTypeConverter
    {
        public static Dictionary<string, ObstacleType> ObstacleTypeMapping = new Dictionary<string, ObstacleType>()
        {
            {("Cube"),(ObstacleType.Cube)},
            {("Sphere"),(ObstacleType.Sphere)},
            {("Cylinder"),(ObstacleType.Cylinder)},
            
        };
    }
}