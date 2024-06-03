using Core.DI;
using Logic.Common;
using Logic.Player;
using UnityEngine;
using View.DI;

namespace View.Player
{
    public class PlayerViewService : MonoBaseWithInject
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _parent;

        private IPlayerSpawnService _playerSpawnService;
        private IPlayerDamageService _playerDamageService;
        private ICollideService _collideService;
        private GameObject _playerInst;

        [Inject]
        public void Construct(IPlayerSpawnService playerSpawnService, IPlayerDamageService playerDamageService,
            ICollideService collideService)
        {
            _playerSpawnService = playerSpawnService;
            _playerDamageService = playerDamageService;
            _collideService = collideService;
        }

        public override void Awake()
        {
            base.Awake();

            _playerSpawnService.OnSpawn += OnSpawn;
        }

        private void OnSpawn(PlayerModel playerModel)
        {
            _playerInst = Instantiate(_playerPrefab, playerModel.Position,
                Quaternion.Euler(0, 0, playerModel.Rotation));
            _playerInst.transform.SetParent(_parent);

            _playerInst.GetComponent<PlayerView>().SetModel(playerModel);
            _playerInst.GetComponent<PlayerUIView>().SetModel(playerModel);
        }

#if DEBUG_COLLIDE
        public void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            foreach (var vert in _collideService.DebugVertices)
            {
                Gizmos.DrawCube(vert, new Vector3(0.05f, 0.05f, 0.05f));
            }
        }
#endif
    }
}
