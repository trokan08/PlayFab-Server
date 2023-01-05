using System.Collections.Generic;

namespace GamePlay.Enums
{
    public enum ObstacleType
    {
        Cube,
        Sphere,
        Cylinder,
        Obstacle5,
        Obstacle7,
        Obstacle3,
        Spike,
    }

    public static class ObstacleTypeConverter
    {
        public static Dictionary<string, ObstacleType> ObstacleTypeMapping = new Dictionary<string, ObstacleType>()
        {
            {("Cube"),(ObstacleType.Cube)},
            {("Sphere"),(ObstacleType.Sphere)},
            {("Cylinder"),(ObstacleType.Cylinder)},
            {("Obstacle5"),(ObstacleType.Obstacle5)},
            {("Obstacle7"),(ObstacleType.Obstacle7)},
            {("Obstacle3"),(ObstacleType.Obstacle3)},
            {("Spike"),(ObstacleType.Spike)},
            
        };
    }
}