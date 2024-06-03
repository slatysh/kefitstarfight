using System.Linq;
using Core.DI;
using Logic.Bullet;
using Logic.Game;
using UnityEngine;

namespace Logic.Common
{
    public class LifetimerService : ILifetimerService, IRunnableService
    {
        private IGameService _gameService;
        private IBulletDamageService _bulletDamageService;

        [Inject]
        public void Construct(IGameService gameService, IBulletDamageService bulletDamageService)
        {
            _gameService = gameService;
            _bulletDamageService = bulletDamageService;
        }

        public void Run()
        {
            var lifetimerList = _gameService.BulletModels.ToList();
            lifetimerList.ForEach(bulletModel =>
            {
                _bulletDamageService.Damage(bulletModel, Time.deltaTime);
            });
        }
    }
}
