using UnityEngine;

namespace Logic.Bullet
{
    public class BulletLaserModel : BulletModel
    {
        public override BulletType BulletType => BulletType.Laser;
        public override Vector2 CollideBox { get; } = new(10, 0.5f);
        public override float HitHp => 0;

        public BulletLaserModel(Vector2 position, float speed, float rotation, float lifetimer)
            : base(position, speed, rotation, lifetimer)
        {
        }
    }
}
