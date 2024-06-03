using UnityEngine;

namespace Logic.Enemy
{
    public class EnemyAsteroidSmallModel : EnemyModel
    {
        public override EnemyType EnemyType => EnemyType.AsteroidSmall;

        public EnemyAsteroidSmallModel(Vector2 position, float speed, float rotation) : base(position, speed, rotation)
        {
        }
    }
}
