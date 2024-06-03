using System;
using Logic.Common;
using Logic.Enemy;
using UnityEngine;

namespace Logic.Bullet
{
    public abstract class BulletModel: ICollidable, IDamagerable, IMoveable
    {
        private float _hp;
        private Vector2 _position;
        private float _speed;
        private float _rotation;
        private float _damage;
        private IMoveStrategy _moveStrategy;
        private event Action<Vector2> _onPositionChange;
        private event Action<float> _onRotationChange;
        private event Action<float> _onHpChange;
        private event Action _onRemove;
        private Vector2 _collideBox = new Vector2(0.25f, 0.25f);

        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                _onPositionChange?.Invoke(_position);
            }
        }

        public float Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                _onRotationChange?.Invoke(_rotation);
            }
        }

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public float Hp
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

        public abstract BulletType BulletType { get; }

        public CollideLayerType CollideLayer => CollideLayerType.Bullet;

        public virtual Vector2 CollideBox => _collideBox;

        public int Damage => 10;

        public virtual float HitHp => _hp;

        public event Action<Vector2> OnPositionChange
        {
            add => _onPositionChange += value;
            remove => _onPositionChange -= value;
        }

        public event Action<float> OnRotationChange
        {
            add => _onRotationChange += value;
            remove => _onRotationChange -= value;
        }

        public event Action<float> OnHpChange
        {
            add => _onHpChange += value;
            remove => _onHpChange -= value;
        }

        public event Action OnRemove
        {
            add => _onRemove += value;
            remove => _onRemove -= value;
        }

        public BulletModel(Vector2 position, float speed, float rotation, float hp)
        {
            _position = position;
            _speed = speed;
            _rotation = rotation;
            _hp = hp;
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
                case CollideLayerType.Enemy:
                    return true;
            }
            return false;
        }
    }
}
