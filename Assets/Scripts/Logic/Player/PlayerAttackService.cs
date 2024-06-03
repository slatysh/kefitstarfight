using System;
using Core.DI;
using Core.Utils;
using Logic.Bullet;
using Logic.Game;
using UnityEngine;

namespace Logic.Player
{
    public class PlayerAttackService : IPlayerAttackService, IRunnableService
    {
        private IBulletSpawnService _bulletSpawnService;
        private IGameService _gameService;
        private float _attackFirstCdTimeout;
        private float _attackFirstOffset;
        private float _attackAdditionalCdTimeout;
        private float _attackAdditionalOffset;
        private float _attackAdditionalCountMax;
        private float _attackAdditionalCountTimeout;

        [Inject]
        public void Construct(IBulletSpawnService bulletSpawnService, IGameService gameService)
        {
            _bulletSpawnService = bulletSpawnService;
            _gameService = gameService;
        }

        public PlayerAttackService(PlayerAttackSettings settings)
        {
            _attackFirstCdTimeout = settings.AttackFirstCdTimeout;
            _attackFirstOffset = settings.AttackFirstOffset;
            _attackAdditionalCdTimeout = settings.AttackAdditionalCdTimeout;
            _attackAdditionalOffset = settings.AttackAdditionalOffset;
            _attackAdditionalCountMax = settings.AttackAdditionalCountMax;
            _attackAdditionalCountTimeout = settings.AttackAdditionalCountTimeout;
        }

        public void Run()
        {
            var playerModel = _gameService.PlayerModel;
            if (playerModel == null)
            {
                return;
            }

            if (playerModel.AttackFirstCd > 0)
            {
                playerModel.AttackFirstCd -= Time.deltaTime;
                if (playerModel.AttackFirstCd < 0)
                {
                    playerModel.AttackFirstCd = 0;
                }
            }

            if (playerModel.AttackAdditionalCd > 0)
            {
                playerModel.AttackAdditionalCd -= Time.deltaTime;
                if (playerModel.AttackAdditionalCd < 0)
                {
                    playerModel.AttackAdditionalCd = 0;
                }
            }

            if (playerModel.AttackAdditionalCountCd > 0 && playerModel.AttackAdditionalCount < _attackAdditionalCountMax)
            {
                playerModel.AttackAdditionalCountCd -= Time.deltaTime;
                if (playerModel.AttackAdditionalCountCd < 0)
                {
                    playerModel.AttackAdditionalCount++;
                    playerModel.AttackAdditionalCountCd = _attackAdditionalCountTimeout;
                }
            }
        }

        public void AttackFirst(bool attack)
        {
            var playerModel = _gameService.PlayerModel;
            if (playerModel == null)
            {
                return;
            }

            if (!attack || playerModel.AttackFirstCd > 0)
            {
                return;
            }

            var offset = _attackFirstOffset * Utils.RotationZToVector(playerModel.Rotation);
            _bulletSpawnService.Spawn(BulletType.Simple, playerModel.Position + offset, playerModel.Rotation);

            playerModel.AttackFirstCd = _attackFirstCdTimeout;
        }

        public void AttackAlternative(bool attack)
        {
            var playerModel = _gameService.PlayerModel;
            if (playerModel == null)
            {
                return;
            }

            if (!attack || playerModel.AttackAdditionalCd > 0 || playerModel.AttackAdditionalCount == 0)
            {
                return;
            }

            var offset = _attackAdditionalOffset * Utils.RotationZToVector(playerModel.Rotation);
            _bulletSpawnService.Spawn(BulletType.Laser, playerModel.Position + offset, playerModel.Rotation);

            playerModel.AttackAdditionalCd = _attackAdditionalCdTimeout;
            playerModel.AttackAdditionalCountCd = _attackAdditionalCountTimeout;
            playerModel.AttackAdditionalCount--;
        }
    }
}
