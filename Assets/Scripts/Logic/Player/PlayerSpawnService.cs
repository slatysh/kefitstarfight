using System;
using Core.DI;
using Logic.Game;
using UnityEngine;

namespace Logic.Player
{
    public class PlayerSpawnService : IPlayerSpawnService
    {
        private IGameService _gameService;
        private Vector2 _spawnPos = new Vector2(0, -4.5f);
        private Action<PlayerModel> _onSpawn;

        public event Action<PlayerModel> OnSpawn
        {
            add => _onSpawn += value;
            remove => _onSpawn -= value;
        }

        [Inject]
        public void Construct(IGameService gameService)
        {
            _gameService = gameService;
        }

        public void Spawn()
        {
            var inst = new PlayerModel(_spawnPos);
            _gameService.SetPlayerModel(inst);
            _onSpawn?.Invoke(inst);
        }
    }
}
