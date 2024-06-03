using System;
using Core.DI;
using Logic.Game;
using UnityEngine;

namespace Logic.Enemy
{
    public class EnemyFactory : IEnemyFactory
    {
        private IGameService _gameService;

        [Inject]
        public void Construct(IGameService gameService)
        {
            _gameService = gameService;
        }

        public EnemyModel Create(EnemyType type, Vector2 pos, float speed, float direction)
        {
            switch (type)
            {
                case EnemyType.Asteroid:
                    var instAsteroid = new EnemyAsteroidModel(pos, speed, direction);
                    instAsteroid.SetMoveStrategy(new MoveDirectStrategy(instAsteroid));
                    return instAsteroid;
                case EnemyType.AsteroidSmall:
                    var instAsteroidSmall = new EnemyAsteroidSmallModel(pos, speed, direction);
                    instAsteroidSmall.SetMoveStrategy(new MoveDirectStrategy(instAsteroidSmall));
                    return instAsteroidSmall;
                case EnemyType.Plate:
                    var instPlate = new EnemyPlateModel(pos, speed, direction);
                    instPlate.SetMoveStrategy(new MoveToPlayerStrategy(instPlate, _gameService.PlayerModel, 90.0f));
                    return instPlate;
            }

            throw new ArgumentException($"No enemy type for type: {type.ToString()}");
        }
    }
}
