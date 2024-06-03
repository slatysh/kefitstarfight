using Logic.Enemy;
using UnityEngine;

namespace Logic.Bullet
{
    public class BulletSimpleModel : BulletModel
    {
        public override BulletType BulletType => BulletType.Simple;

        public BulletSimpleModel(Vector2 position, float speed, float rotation, float lifetimer)
            : base(position, speed, rotation, lifetimer)
        {
        }
    }
}
