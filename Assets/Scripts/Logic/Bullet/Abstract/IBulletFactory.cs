using UnityEngine;

namespace Logic.Bullet
{
    public interface IBulletFactory
    {
        BulletModel Create(BulletType type, Vector2 pos, float direction);
    }
}
