using System;
using Core.DI;
using Core.Utils;
using Logic.Game;
using UnityEditor.Graphs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Logic.Enemy
{
    public class EnemySpawnService : IEnemySpawnService, IRunnableService
    {
        private IEnemyFactory _enemyFactory;
        private IGameService _gameService;

        private event Action<EnemyModel> _onSpawn;
        private Vector2 _areaLeftTop;
        private Vector2 _areaRightBottom;
        private Vector2 _delayRange;
        private Vector2 _directionRange;
        private Vector2 _speedRange;
        private bool _isStarted = false;
        private float _timer = 2.0f;

        public event Action<EnemyModel> OnSpawn
        {
            add => _onSpawn += value;
            remove => _onSpawn -= value;
        }

        [Inject]
        public void Construct(IEnemyFactory enemyFactory, IGameService gameService)
        {
            _enemyFactory = enemyFactory;
            _gameService = gameService;
        }

        public EnemySpawnService(EnemySpawnSettings settings)
        {
            _areaLeftTop = settings.AreaLeftTop;
            _areaRightBottom = settings.AreaRightBottom;
            _delayRange = settings.DelayRange;
            _directionRange = settings.DirectionRange;
            _speedRange = settings.SpeedRange;
        }

        public void Run()
        {
            if (!_isStarted)
            {
                return;
            }

            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                var delay = Random.Range(_delayRange.x, _delayRange.y);
                Spawn();
                _timer = (int)delay;
            }
        }

        public void Start()
        {
            _isStarted = true;
        }

        public void Stop()
        {
            _isStarted = false;
        }

        private void Spawn()
        {
            var type = Utils.RandomEnum<EnemyType>();
            var pos = Utils.RandomVector2(_areaLeftTop, _areaRightBottom);
            var direction = Random.Range(_directionRange.x, _directionRange.y);
            var speed = Random.Range(_speedRange.x, _speedRange.y);

            Spawn(type, pos, speed, direction);
        }

        public void Spawn(EnemyType type, Vector2 pos, float speed, float direction)
        {
            var inst = _enemyFactory.Create(type, pos, speed, direction);
            _gameService.AddEnemyModel(inst);
            _onSpawn?.Invoke(inst);
        }
    }
}
