using System;
using UnityEngine;

namespace Logic.Bullet
{
    public interface IBulletSpawnService
    {
        event Action<BulletModel> OnSpawn;
        void Spawn(BulletType type, Vector2 pos, float direction);
    }
}
