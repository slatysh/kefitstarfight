using System;
using System.Collections.Generic;
using Core.DI;
using Logic.Bullet;
using Logic.Game;
using UnityEngine;
using View.Common;
using View.DI;

namespace View.Bullet
{
    public class BulletViewService : MonoBaseWithInject
    {
        [SerializeField] private List<BulletPrefabInfo> _bulletPrefabInfos;
        [SerializeField] private Transform _parent;

        private IBulletSpawnService _bulletSpawnService;
        private IGameService _gameService;
        private Dictionary<BulletType, PoolBase<BulletView>> _pools;

        [Inject]
        public void Construct(IGameService gameService, IBulletSpawnService bulletSpawnService)
        {
            _bulletSpawnService = bulletSpawnService;
            _gameService = gameService;
        }

        public override void Awake()
        {
            base.Awake();

            _pools = new Dictionary<BulletType, PoolBase<BulletView>>();
            foreach (var bulletPrefabInfo in _bulletPrefabInfos)
            {
                _pools.Add(bulletPrefabInfo.BulletType,
                    new PoolBase<BulletView>(bulletPrefabInfo.Prefab.GetComponent<BulletView>(), _parent, 5));
            }

            _bulletSpawnService.OnSpawn += OnSpawn;
            _gameService.OnGameOver += OnGameOver;
        }

        private void OnSpawn(BulletModel bulletModel)
        {
            var pool = _pools[bulletModel.BulletType];
            var bulletInst = pool.Get();
            var bulletInstTransform = bulletInst.transform;
            bulletInstTransform.position = bulletModel.Position;
            bulletInstTransform.rotation = Quaternion.Euler(0, 0, bulletModel.Rotation);
            bulletInstTransform.SetParent(_parent);
            bulletInst.GetComponent<BulletView>().SetModel(bulletModel, pool);
        }

        private void OnGameOver(int score)
        {
            foreach (var pool in _pools.Values)
            {
                pool.Reset();
            }
        }


        [Serializable]
        public class BulletPrefabInfo
        {
            public BulletType BulletType;
            public GameObject Prefab;
        }
    }
}
