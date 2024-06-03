using Core.DI;
using Core.Utils;
using Logic.Game;
using UnityEngine;

namespace Logic.Player
{
    public class PlayerMoveService : IPlayerMoveService, IRunnableService
    {
        private IGameService _gameService;

        private float _directSpeedAcceleration;
        private float _directSpeedDeceleration;
        private float _directSpeedMax;
        private float _rotationSpeedAcceleration;
        private float _rotationSpeedDeceleration;
        private float _rotationSpeedMax;
        private Vector2 _moveVector = Vector2.zero;
        private float _minVal = 0.00001f;
        private Vector2 _teleportScreen = Vector2.zero;

        [Inject]
        public void Construct(IGameService gameService)
        {
            _gameService = gameService;

            _teleportScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height)) * 2;
        }

        public PlayerMoveService(PlayerMoveSettings settings)
        {
            _directSpeedAcceleration = settings.DirectSpeedAcceleration;
            _directSpeedDeceleration = settings.DirectSpeedDeceleration;
            _directSpeedMax = settings.DirectSpeedMax;
            _rotationSpeedAcceleration = settings.RotationSpeedAcceleration;
            _rotationSpeedDeceleration = settings.RotationSpeedDeceleration;
            _rotationSpeedMax = settings.RotationSpeedMax;
        }

        public void Run()
        {
            var playerModel = _gameService.PlayerModel;
            if (playerModel == null)
            {
                return;
            }

            var rotationSpeedOld = playerModel.RotationSpeed;
            var directSpeedOld = playerModel.DirectSpeed;

            if (_moveVector.x < 0)
            {
                playerModel.RotationSpeed += _rotationSpeedAcceleration;
                if (playerModel.RotationSpeed > _rotationSpeedMax)
                {
                    playerModel.RotationSpeed = _rotationSpeedMax;
                }
            }
            else if (_moveVector.x > 0)
            {
                playerModel.RotationSpeed -= _rotationSpeedAcceleration;
                if (playerModel.RotationSpeed < -_rotationSpeedMax)
                {
                    playerModel.RotationSpeed = -_rotationSpeedMax;
                }
            }
            else
            {
                var rotationSpeedAbs = Mathf.Abs(playerModel.RotationSpeed);
                if (rotationSpeedAbs < _minVal)
                {
                    playerModel.RotationSpeed = 0;
                }
                else
                {
                    var rotationSpeedAbsNew = rotationSpeedAbs - _rotationSpeedDeceleration;
                    if (rotationSpeedAbsNew < _minVal)
                    {
                        rotationSpeedAbsNew = 0.0f;
                    }

                    playerModel.RotationSpeed =
                        rotationSpeedAbsNew * Mathf.Sign(_gameService.PlayerModel.RotationSpeed);
                }
            }

            if (_moveVector.y > 0)
            {
                playerModel.DirectSpeed += _directSpeedAcceleration;
                if (playerModel.DirectSpeed > _directSpeedMax)
                {
                    playerModel.DirectSpeed = _directSpeedMax;
                }
            }
            else
            {
                var directSpeedAbs = Mathf.Abs(playerModel.DirectSpeed);
                if (directSpeedAbs < _minVal)
                {
                    playerModel.DirectSpeed = 0;
                }
                else
                {
                    var directSpeedAbsNew = directSpeedAbs - _directSpeedDeceleration;
                    playerModel.DirectSpeed = directSpeedAbsNew;
                }
            }

            if (Mathf.Abs(playerModel.RotationSpeed) > _minVal)
            {
                var a = playerModel.RotationSpeed - rotationSpeedOld;
                playerModel.Rotation += playerModel.RotationSpeed * Time.deltaTime;
            }

            if (Mathf.Abs(playerModel.DirectSpeed) > _minVal)
            {
                var a = playerModel.DirectSpeed - directSpeedOld;
                var direction = Utils.RotationZToVector(playerModel.Rotation);
                playerModel.Position += direction * ((playerModel.DirectSpeed) * Time.deltaTime);

                var nearScreenBorder = Utils.CheckNearScreenBorder(playerModel.Position, -5);
                if (nearScreenBorder.HDir == Utils.Dir.Left)
                {
                    playerModel.Position += new Vector2(_teleportScreen.x, 0);
                }
                else if (nearScreenBorder.HDir == Utils.Dir.Right)
                {
                    playerModel.Position += new Vector2(-_teleportScreen.x, 0);
                }

                if (nearScreenBorder.VDir == Utils.Dir.Top)
                {
                    playerModel.Position += new Vector2(0, _teleportScreen.y);
                }
                else if (nearScreenBorder.VDir == Utils.Dir.Bottom)
                {
                    playerModel.Position += new Vector2(0, -_teleportScreen.y);
                }
            }

            _moveVector = Vector2.zero;
        }

        public void Move(Vector2 moveVector)
        {
            _moveVector = moveVector;
        }
    }
}
