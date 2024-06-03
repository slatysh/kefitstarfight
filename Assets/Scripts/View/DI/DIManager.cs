using Core.DI;
using Logic.Bullet;
using Logic.Common;
using Logic.Enemy;
using Logic.Game;
using Logic.Player;
using UnityEngine;

namespace View.DI
{
    public class DIManager : MonoBehaviour
    {
        [SerializeField] private CommonSettings settings;

        public static DIContainer Container { get; private set; }

        private void Awake()
        {
            Container = new DIContainer();

            Container.Register<ICollideService>(new CollideService());
            Container.Register<ILifetimerService>(new LifetimerService());

            Container.Register<IGameService>(new GameService());

            Container.Register<IPlayerSpawnService>(new PlayerSpawnService());
            Container.Register<IPlayerMoveService>(new PlayerMoveService(settings.PlayerMoveSettings));
            Container.Register<IPlayerDamageService>(new PlayerDamageService());
            Container.Register<IPlayerAttackService>(new PlayerAttackService(settings.PlayerAttackSettings));

            Container.Register<IEnemyFactory>(new EnemyFactory());
            Container.Register<IEnemySpawnService>(new EnemySpawnService(settings.EnemySpawnSettings));
            Container.Register<IEnemyMoveService>(new EnemyMoveService());
            Container.Register<IEnemyDamageService>(new EnemyDamageService());

            Container.Register<IBulletFactory>(new BulletFactory(settings.BulletSettingsDict));
            Container.Register<IBulletSpawnService>(new BulletSpawnService());
            Container.Register<IBulletMoveService>(new BulletMoveService());
            Container.Register<IBulletDamageService>(new BulletDamageService());

            Container.Init();
        }

        private void Update()
        {
            Container?.Run();
        }
    }
}
