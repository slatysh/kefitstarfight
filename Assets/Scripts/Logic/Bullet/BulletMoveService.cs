using Core.DI;
using Logic.Game;

namespace Logic.Bullet
{
    public class BulletMoveService : IBulletMoveService, IRunnableService
    {
        private IGameService _gameService;

        [Inject]
        public void Construct(IGameService gameService)
        {
            _gameService = gameService;
        }

        public void Run()
        {
            _gameService.BulletModels.ForEach(bullet => { bullet.Move(); });
        }
    }
}
