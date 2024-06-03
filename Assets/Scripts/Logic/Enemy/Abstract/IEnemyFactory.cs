using UnityEngine;

namespace Logic.Enemy
{
    public interface IEnemyFactory
    {
        EnemyModel Create(EnemyType type, Vector2 pos, float speed, float direction);
    }
}
