using System;
using Logic.Common;
using UnityEngine;

namespace Logic.Enemy
{
    public abstract class EnemyModel : ICollidable, IDamagerable, IMoveable
    {
        private int _hp;
        private Vector2 _position;
        private float _speed;
        private float _rotation;
        private IMoveStrategy _moveStrategy;
        private event Action<Vector2> _onPositionChange;
        private event Action<int> _onHpChange;
        private event Action _onRemove;

        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                _onPositionChange?.Invoke(_position);
            }
        }

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public float Rotation
        {
            get => _rotation;
            set => _rotation = value;
        }

        public int Hp
        {
            get => _hp;
            set
            {
                _hp = value;
                if (_hp <= 0)
                {
                    _hp = 0;
                }

                _onHpChange?.Invoke(_hp);
            }
        }

        public abstract EnemyType EnemyType { get; }

        public CollideLayerType CollideLayer => CollideLayerType.Enemy;

        public virtual Vector2 CollideBox { get; } =  new Vector2(1f, 1f);

        public virtual int Damage => 1;

        public event Action<Vector2> OnPositionChange
        {
            add => _onPositionChange += value;
            remove => _onPositionChange -= value;
        }

        public event Action<int> OnHpChange
        {
            add => _onHpChange += value;
            remove => _onHpChange -= value;
        }

        public event Action OnRemove
        {
            add => _onRemove += value;
            remove => _onRemove -= value;
        }

        public EnemyModel(Vector2 position, float speed, float rotation)
        {
            _position = position;
            _speed = speed;
            _rotation = rotation;
        }

        public void SetMoveStrategy(IMoveStrategy moveStrategy)
        {
            _moveStrategy = moveStrategy;
        }

        public void Move()
        {
            _moveStrategy?.Move();
        }

        public void Remove()
        {
            _onRemove?.Invoke();
        }

        public bool IsCollidableBy(CollideLayerType sourceLayerType)
        {
            switch (sourceLayerType)
            {
                case CollideLayerType.Player:
                    return true;
                case CollideLayerType.Bullet:
                    return true;
            }

            return false;
        }
    }
}
