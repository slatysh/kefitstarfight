using System;
using UnityEngine;

namespace Logic.Enemy
{
    public interface IEnemySpawnService
    {
        void Start();
        void Stop();
        void Spawn(EnemyType type, Vector2 pos, float speed, float direction);
        event Action<EnemyModel> OnSpawn;
    }
}
