using System;
using Core.DI;
using Logic.Game;
using UnityEngine;

namespace Logic.Bullet
{
    public class BulletSpawnService : IBulletSpawnService
    {
        private IBulletFactory _bulletFactory;
        private IGameService _gameService;

        private event Action<BulletModel> _onSpawn;

        public event Action<BulletModel> OnSpawn
        {
            add => _onSpawn += value;
            remove => _onSpawn -= value;
        }

        [Inject]
        public void Construct(IBulletFactory bulletFactory, IGameService gameService)
        {
            _bulletFactory = bulletFactory;
            _gameService = gameService;
        }

        public void Spawn(BulletType type, Vector2 pos, float direction)
        {
            var inst = _bulletFactory.Create(type, pos, direction);
            _gameService.AddBulletModel(inst);
            _onSpawn?.Invoke(inst);
        }
    }
}
