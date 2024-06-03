using Core.Utils;
using Logic.Common;
using Logic.Player;
using UnityEngine;

namespace Logic.Enemy
{
    public class MoveToPlayerStrategy : IMoveStrategy
    {
        private IMoveable _target;
        private Vector2 _speedDirected;
        private float _maxRotationSpeed;
        private PlayerModel _playerModel;

        public MoveToPlayerStrategy(IMoveable target, PlayerModel playerModel, float maxRotationSpeed)
        {
            _target = target;
            _speedDirected = target.Speed * Utils.RotationZToVector(target.Rotation);
            _playerModel = playerModel;
            _maxRotationSpeed = maxRotationSpeed;
        }

        public void Move()
        {
            var angleToTarget = Vector2.SignedAngle(Vector2.right, _playerModel.Position - _target.Position);
            _target.Rotation = Mathf.MoveTowardsAngle(_target.Rotation, angleToTarget, _maxRotationSpeed * Time.deltaTime);
            _speedDirected = _target.Speed * Utils.RotationZToVector(_target.Rotation);
            _target.Position += _speedDirected * Time.deltaTime;
        }
    }
}
