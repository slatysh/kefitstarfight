using Core.DI;
using Logic.Game;

namespace Logic.Bullet
{
    public class BulletDamageService : IBulletDamageService
    {
        private IGameService _gameService;

        [Inject]
        public void Construct(IGameService gameService)
        {
            _gameService = gameService;
        }

        public void Hit(BulletModel bulletModel)
        {
            Damage(bulletModel, bulletModel.HitHp);
        }

        public void Damage(BulletModel bulletModel, float damage)
        {
            bulletModel.Hp -= damage;

            if (bulletModel.Hp <= 0)
            {
                _gameService.RemoveBulletModel(bulletModel);
            }
        }
    }
}
