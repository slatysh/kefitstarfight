using System;
using System.Collections.Generic;
using Core.DI;
using Core.Utils;
using Logic.Bullet;
using Logic.Enemy;
using Logic.Game;
using Logic.Player;
using UnityEngine;

namespace Logic.Common
{
    public class CollideService : ICollideService, IRunnableService
    {
        private IGameService _gameService;
        private IEnemyDamageService _enemyDamageService;
        private IPlayerDamageService _playerDamageService;
        private IBulletDamageService _bulletDamageService;

        [Inject]
        public void Construct(IGameService gameService,
            IEnemyDamageService enemyDamageService,
            IPlayerDamageService playerDamageService,
            IBulletDamageService bulletDamageService)
        {
            _gameService = gameService;
            _enemyDamageService = enemyDamageService;
            _playerDamageService = playerDamageService;
            _bulletDamageService = bulletDamageService;
        }

#if DEBUG_COLLIDE

        private List<Vector2> _debugDebugVertices = new();
        public List<Vector2> DebugVertices => _debugDebugVertices;
#endif

        public void Run()
        {
#if DEBUG_COLLIDE
            _debugDebugVertices.Clear();
#endif

            var player = _gameService.PlayerModel;
            if (player == null)
            {
                return;
            }

            var collidesEnemyBullet = new List<(EnemyModel, BulletModel)>();
            _gameService.BulletModels.ForEach(bullet =>
            {
                _gameService.EnemyModels.ForEach(enemy =>
                {
                    if (Collide(bullet, bullet.Position, bullet.Rotation, enemy, enemy.Position, 0))
                    {
                        collidesEnemyBullet.Add(new ValueTuple<EnemyModel, BulletModel>(enemy, bullet));
                    }
                });
            });

            var collidesEnemyPlayerOrBorder = new List<(EnemyModel, PlayerModel)>();
            _gameService.EnemyModels.ForEach(enemy =>
            {
                if (Collide(enemy, enemy.Position, 0, player, player.Position, player.Rotation))
                {
                    collidesEnemyPlayerOrBorder.Add(new ValueTuple<EnemyModel, PlayerModel>(enemy, player));
                    return;
                }

                var nearScreenBorders = Utils.CheckNearScreenBorder(enemy.Position, -100);
                if (nearScreenBorders.HDir != Utils.Dir.No || nearScreenBorders.VDir != Utils.Dir.No)
                {
                    collidesEnemyPlayerOrBorder.Add(new ValueTuple<EnemyModel, PlayerModel>(enemy, null));
                }
            });

            collidesEnemyBullet.ForEach(ebc =>
            {
                var enemy = ebc.Item1;
                var bullet = ebc.Item2;
                _enemyDamageService.Damage(enemy, enemy.Hp, true);
                _bulletDamageService.Hit(bullet);
            });

            collidesEnemyPlayerOrBorder.ForEach(ebc =>
            {
                var enemy = ebc.Item1;
                var playerCur = ebc.Item2;
                _enemyDamageService.Damage(enemy, enemy.Hp);
                if (playerCur != null)
                {
                    _playerDamageService.Damage(enemy.Damage);
                }
            });
        }

        private bool Collide(ICollidable source, Vector2 sourcePos, float sourceRotation, ICollidable target,
            Vector2 targetPos, float targetRotation)
        {
            var rotatedRectangle1 =
                new CollisionUtils.RotatedRectangle(sourcePos, source.CollideBox.x, source.CollideBox.y,
                    sourceRotation);
            var rotatedRectangle2 =
                new CollisionUtils.RotatedRectangle(targetPos, target.CollideBox.x, target.CollideBox.y,
                    targetRotation);

#if DEBUG_COLLIDE
            _debugDebugVertices.AddRange(rotatedRectangle1.GetVertices());
            _debugDebugVertices.AddRange(rotatedRectangle2.GetVertices());
#endif

            if (target.IsCollidableBy(source.CollideLayer) &&
                CollisionUtils.CheckRectanglesIntersect(rotatedRectangle1, rotatedRectangle2))
            {
                return true;
            }

            return false;
        }
    }
}
