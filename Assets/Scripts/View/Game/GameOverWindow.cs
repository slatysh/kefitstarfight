using Core.DI;
using Logic.Game;
using TMPro;
using UnityEngine;
using View.DI;

namespace View.Game
{
    public class GameOverWindow : MonoBaseWithInject
    {
        [SerializeField] private TextMeshProUGUI scoreTT;

        private IGameService _gameService;

        [Inject]
        public void Construct(IGameService gameService)
        {
            _gameService = gameService;
        }

        public void Open(int score)
        {
            scoreTT.text = $"Score: {score}";
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void TryAgain()
        {
            Close();
            _gameService.StartGame();
        }
    }
}
