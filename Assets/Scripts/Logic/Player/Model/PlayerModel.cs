using System;
using Logic.Common;
using UnityEngine;

namespace Logic.Player
{
    public class PlayerModel : ICollidable
    {
        private Vector2 _position;
        private float _rotation;
        private int _hp;
        private float _directSpeed;
        private float _rotationSpeed;
        private float _attackAdditionalCountCd;
        private int _attackAdditionalCount;
        private event Action<Vector2> _onPositionChange;
        private event Action<float> _onRotationChange;
        private event Action<int> _onHpChange;
        private event Action _onRemove;
        private event Action<float> _onDirectSpeedChange;
        private event Action<float> _onRotationSpeedChange;
        private event Action<float> _onAttackAdditionalCountCdChange;
        private event Action<int> _onAttackAdditionalCountChange;

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

        public float DirectSpeed
        {
            get => _directSpeed;
            set
            {
                _directSpeed = value;
                _onDirectSpeedChange?.Invoke(_directSpeed);
            }
        }

        public float RotationSpeed
        {
            get => _rotationSpeed;
            set
            {
                _rotationSpeed = value;
                _onRotationSpeedChange?.Invoke(_rotationSpeed);
            }
        }

        public float AttackFirstCd { get; set; } = 0.0f;

        public float AttackAdditionalCountCd
        {
            get => _attackAdditionalCountCd;
            set
            {
                _attackAdditionalCountCd = value;
                _onAttackAdditionalCountCdChange?.Invoke(_attackAdditionalCountCd);
            }
        }

        public int AttackAdditionalCount
        {
            get => _attackAdditionalCount;
            set
            {
                _attackAdditionalCount = value;
                _onAttackAdditionalCountChange?.Invoke(_attackAdditionalCount);
            }
        }

        public float AttackAdditionalCd { get; set; } = 0.0f;

        public CollideLayerType CollideLayer => CollideLayerType.Player;

        public Vector2 CollideBox { get; } = new(1f, 1f);

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

        public event Action<float> OnDirectSpeedChange
        {
            add => _onDirectSpeedChange += value;
            remove => _onDirectSpeedChange -= value;
        }

        public event Action<float> OnRotationSpeedChange
        {
            add => _onRotationSpeedChange += value;
            remove => _onRotationSpeedChange -= value;
        }

        public event Action<float> OnAttackAdditionalCountCdChange
        {
            add => _onAttackAdditionalCountCdChange += value;
            remove => _onAttackAdditionalCountCdChange -= value;
        }

        public event Action<int> OnAttackAdditionalCountChange
        {
            add => _onAttackAdditionalCountChange += value;
            remove => _onAttackAdditionalCountChange -= value;
        }

        public PlayerModel(Vector2 position)
        {
            _position = position;
            _rotation = 90.0f;
            _attackAdditionalCount = 3;
            _hp = 3;
        }

        public void Remove()
        {
            _onRemove?.Invoke();
        }

        public void Collide()
        {
        }

        public bool IsCollidableBy(CollideLayerType sourceLayerType)
        {
            switch (sourceLayerType)
            {
                case CollideLayerType.Enemy:
                    return true;
                case CollideLayerType.Bullet:
                    return true;
            }

            return false;
        }
    }
}
