using Core.DI;
using Logic.Game;
using UnityEngine;
using View.DI;

namespace View.Game
{
    public class GameViewService : MonoBaseWithInject
    {
        [SerializeField] private GameObject _gamePrefab;
        [SerializeField] private Transform _parent;

        private IGameService _gameService;

        private GameUIView _gameInst;

        [Inject]
        public void Construct(IGameService gameService)
        {
            _gameService = gameService;
        }

        public override void Awake()
        {
            base.Awake();

            _gameInst = Instantiate(_gamePrefab, Vector2.zero, Quaternion.identity).GetComponent<GameUIView>();
            _gameInst.transform.SetParent(_parent);

            _gameService.OnStartGame += OnStartGame;
            _gameService.OnGameOver += OnGameOver;

            StartGame();
        }

        public void StartGame()
        {
            _gameService.StartGame();
        }

        private void OnGameOver(int score)
        {
            _gameInst.GameOver(score);
        }

        private void OnStartGame()
        {
            _gameInst.SetModel(_gameService.GameModel, _gameService.PlayerModel);
        }
    }
}
