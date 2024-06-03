using System;
using System.Collections.Generic;
using Core.DI;
using Logic.Bullet;
using Logic.Enemy;
using Logic.Player;

namespace Logic.Game
{
    public class GameService : IGameService
    {
        private IPlayerSpawnService _playerSpawnService;
        private IEnemySpawnService _enemySpawnService;

        private GameModel _gameModel;
        private PlayerModel _playerModel;
        private List<BulletModel> _bulletModels;
        private List<EnemyModel> _enemyModels;
        private event Action<int> _onGameOver;
        private event Action _onStartGame;

        public GameModel GameModel => _gameModel;
        public PlayerModel PlayerModel => _playerModel;
        public List<BulletModel> BulletModels => _bulletModels;
        public List<EnemyModel> EnemyModels => _enemyModels;

        [Inject]
        public void Construct(IPlayerSpawnService playerSpawnService, IEnemySpawnService enemySpawnService)
        {
            _playerSpawnService = playerSpawnService;
            _enemySpawnService = enemySpawnService;
        }

        public void StartGame()
        {
            _gameModel = new GameModel();
            _playerModel = null;
            _bulletModels = new List<BulletModel>();
            _enemyModels = new List<EnemyModel>();

            _playerSpawnService.Spawn();
            _enemySpawnService.Start();

            _onStartGame?.Invoke();
        }

        public void ScoreAdd(int score)
        {
            _gameModel.Score += score;
        }

        public void SetPlayerModel(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        public void RemovePlayerModel()
        {
            _playerModel?.Remove();
            _playerModel = null;
        }

        public void AddBulletModel(BulletModel bulletModel)
        {
            _bulletModels.Add(bulletModel);
        }

        public void RemoveBulletModel(BulletModel bulletModel)
        {
            bulletModel.Remove();
            _bulletModels.Remove(bulletModel);
        }

        public void AddEnemyModel(EnemyModel enemyModel)
        {
            _enemyModels.Add(enemyModel);
        }

        public void RemoveEnemyModel(EnemyModel enemyModel)
        {
            enemyModel.Remove();
            _enemyModels.Remove(enemyModel);
        }

        public event Action<int> OnGameOver
        {
            add => _onGameOver += value;
            remove => _onGameOver -= value;
        }

        public event Action OnStartGame
        {
            add => _onStartGame += value;
            remove => _onStartGame -= value;
        }

        public void GameOver()
        {
            _onGameOver?.Invoke(_gameModel.Score);
            _enemySpawnService.Stop();
        }
    }
}
