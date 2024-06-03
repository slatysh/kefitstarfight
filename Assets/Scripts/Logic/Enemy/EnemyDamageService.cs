using Core.DI;
using Logic.Game;
using UnityEngine;

namespace Logic.Enemy
{
    public class EnemyDamageService : IEnemyDamageService
    {
        private IGameService _gameService;
        private IEnemySpawnService _enemySpawnService;

        [Inject]
        public void Construct(IGameService gameService, IEnemySpawnService enemySpawnService)
        {
            _gameService = gameService;
            _enemySpawnService = enemySpawnService;
        }

        public void Damage(EnemyModel enemyModel, int damage, bool isAddScore = false)
        {
            enemyModel.Hp -= damage;

            if (enemyModel.Hp <= 0)
            {
                _gameService.RemoveEnemyModel(enemyModel);
                if (isAddScore)
                {
                    _gameService.ScoreAdd(1);
                }

                if (enemyModel is IEnemyRecreateOnDestroyAble recreateOnDestroyAble)
                {
                    var recreateInfo = recreateOnDestroyAble.GetSpawnInfo();
                    for (var i = 0; i < recreateInfo.Item2; i++)
                    {
                        var direction = enemyModel.Rotation + Random.Range(-recreateInfo.Item3 / 2, recreateInfo.Item3 / 2);
                        _enemySpawnService.Spawn(recreateInfo.Item1, enemyModel.Position, enemyModel.Speed, direction);
                    }
                }
            }
        }
    }
}
