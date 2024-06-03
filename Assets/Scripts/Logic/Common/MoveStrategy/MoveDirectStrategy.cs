using Core.Utils;
using Logic.Common;
using UnityEngine;

namespace Logic.Enemy
{
    public class MoveDirectStrategy : IMoveStrategy
    {
        private IMoveable _target;
        private Vector2 _speedDirected;

        public MoveDirectStrategy(IMoveable target)
        {
            _target = target;
            _speedDirected = target.Speed * Utils.RotationZToVector(target.Rotation);
        }

        public void Move()
        {
            _target.Position += _speedDirected * Time.deltaTime;
        }
    }
}
