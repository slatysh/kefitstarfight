using UnityEngine;

namespace Logic.Enemy
{
    public class EnemyAsteroidModel : EnemyModel, IEnemyRecreateOnDestroyAble
    {
        public override EnemyType EnemyType => EnemyType.Asteroid;

        public EnemyAsteroidModel(Vector2 position, float speed, float rotation) : base(position, speed, rotation)
        {
        }

        public (EnemyType, int, float) GetSpawnInfo()
        {
            //Type, Cound, AngleArc,
            return (EnemyType.AsteroidSmall, 2, 30.0f);
        }
    }
}
