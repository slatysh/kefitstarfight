namespace Logic.Bullet
{
    public interface IBulletDamageService
    {
        void Hit(BulletModel bulletModel);
        void Damage(BulletModel bulletModel, float damage);
    }
}
