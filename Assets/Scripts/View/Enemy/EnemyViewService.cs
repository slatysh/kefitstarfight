using System;
using System.Collections.Generic;
using Core.DI;
using Logic.Enemy;
using Logic.Game;
using UnityEngine;
using View.Common;
using View.DI;

namespace View.Enemy
{
    public class EnemyViewService : MonoBaseWithInject
    {
        [SerializeField] private List<EnemyPrefabInfo> _enemyPrefabInfos;
        [SerializeField] private Transform _parent;

        private IEnemySpawnService _enemySpawnService;
        private IGameService _gameService;
        private Dictionary<EnemyType, PoolBase<EnemyView>> _pools;

        [Inject]
        public void Construct(IGameService gameService, IEnemySpawnService enemySpawnService)
        {
            _enemySpawnService = enemySpawnService;
            _gameService = gameService;
        }

        public override void Awake()
        {
            base.Awake();

            _pools = new Dictionary<EnemyType, PoolBase<EnemyView>>();
            foreach (var enemyPrefabInfo in _enemyPrefabInfos)
            {
                _pools.Add(enemyPrefabInfo.EnemyType,
                    new PoolBase<EnemyView>(enemyPrefabInfo.Prefab.GetComponent<EnemyView>(), _parent, 5));
            }

            _enemySpawnService.OnSpawn += OnSpawn;
            _gameService.OnGameOver += OnGameOver;
        }

        private void OnSpawn(EnemyModel enemyModel)
        {
            var pool = _pools[enemyModel.EnemyType];
            var enemyInst = pool.Get();
            var enemyInstTransform = enemyInst.transform;
            enemyInstTransform.position = enemyModel.Position;
            enemyInstTransform.rotation = Quaternion.identity;
            enemyInstTransform.SetParent(_parent);
            enemyInst.GetComponent<EnemyView>().SetModel(enemyModel, pool);
        }

        private void OnGameOver(int score)
        {
            foreach (var pool in _pools.Values)
            {
                pool.Reset();
            }
        }


        [Serializable]
        public class EnemyPrefabInfo
        {
            public EnemyType EnemyType;
            public GameObject Prefab;
        }
    }
}
