using Core.DI;
using Logic.Game;

namespace Logic.Player
{
    public class PlayerDamageService : IPlayerDamageService
    {
        private IGameService _gameService;

        [Inject]
        public void Construct(IGameService gameService)
        {
            _gameService = gameService;
        }

        public void Damage(int damage)
        {
            var playerModel = _gameService.PlayerModel;
            if (playerModel == null)
            {
                return;
            }

            playerModel.Hp -= damage;

            if (playerModel.Hp == 0)
            {
                _gameService.RemovePlayerModel();
                _gameService.GameOver();
            }
        }
    }
}
