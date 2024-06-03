using Core.DI;
using Logic.Game;

namespace Logic.Enemy
{
    public class EnemyMoveService : IEnemyMoveService, IRunnableService
    {
        private IGameService _gameService;

        [Inject]
        public void Construct(IGameService gameService)
        {
            _gameService = gameService;
        }

        public void Run()
        {
            _gameService.EnemyModels.ForEach(enemy => { enemy.Move(); });
        }
    }
}
