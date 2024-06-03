using UnityEngine;

namespace Logic.Enemy
{
    [CreateAssetMenu(fileName = "EnemySpawnSettings", menuName = "Settings/EnemySpawnSettings", order = 1)]
    public class EnemySpawnSettings : ScriptableObject
    {
        public Vector2 AreaLeftTop = new Vector2(-7, 4.5f);
        public Vector2 AreaRightBottom = new Vector2(7, 4.5f);
        public Vector2 DelayRange = new(2.0f, 2.0f);
        public Vector2 DirectionRange = new(-150, -30);
        public Vector2 SpeedRange = new(0.01f, 0.02f);
    }
}
