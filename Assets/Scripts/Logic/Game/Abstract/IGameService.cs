using System;
using System.Collections.Generic;
using Logic.Bullet;
using Logic.Enemy;
using Logic.Player;

namespace Logic.Game
{
    public interface IGameService
    {
        GameModel GameModel { get; }
        PlayerModel PlayerModel { get; }
        List<BulletModel> BulletModels { get; }
        List<EnemyModel> EnemyModels { get; }
        event Action<int> OnGameOver;
        event Action OnStartGame;

        void StartGame();
        void ScoreAdd(int score);
        void SetPlayerModel(PlayerModel playerModel);
        void RemovePlayerModel();
        void AddBulletModel(BulletModel bulletModel);
        void RemoveBulletModel(BulletModel bulletModel);
        void AddEnemyModel(EnemyModel enemyModel);
        void RemoveEnemyModel(EnemyModel enemyModel);
        void GameOver();
    }
}
