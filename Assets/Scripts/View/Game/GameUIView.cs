using Logic.Game;
using Logic.Player;
using TMPro;
using UnityEngine;

namespace View.Game
{
    public class GameUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coordsTT;
        [SerializeField] private TextMeshProUGUI angleTT;
        [SerializeField] private TextMeshProUGUI speedTT;
        [SerializeField] private TextMeshProUGUI laserCountTT;
        [SerializeField] private TextMeshProUGUI laserCdTT;
        [SerializeField] private TextMeshProUGUI hpTT;
        [SerializeField] private TextMeshProUGUI scoreTT;
        [SerializeField] private GameOverWindow gameOverWindow;

        private GameModel _gameModel;
        private PlayerModel _playerModel;

        public void SetModel(GameModel gameModel, PlayerModel playerModel)
        {
            _gameModel = gameModel;
            _playerModel = playerModel;

            _playerModel.OnPositionChange += OnPositionChange;
            _playerModel.OnRotationChange += OnRotationChange;
            _playerModel.OnDirectSpeedChange += OnDirectSpeedChange;
            _playerModel.OnAttackAdditionalCountCdChange += OnAttackAdditionalCountCdChange;
            _playerModel.OnAttackAdditionalCountChange += OnAttackAdditionalCountChange;
            _playerModel.OnHpChange += OnHpChange;
            _gameModel.OnScoreChange += OnScoreChange;

            OnPositionChange(_playerModel.Position);
            OnRotationChange(_playerModel.Rotation);
            OnDirectSpeedChange(_playerModel.DirectSpeed);
            OnAttackAdditionalCountCdChange(_playerModel.AttackAdditionalCountCd);
            OnAttackAdditionalCountChange(_playerModel.AttackAdditionalCount);
            OnHpChange(_playerModel.Hp);
            OnScoreChange(_gameModel.Score);
        }

        public void OnDestroy()
        {
            if (_playerModel != null)
            {
                _playerModel.OnPositionChange -= OnPositionChange;
                _playerModel.OnRotationChange -= OnRotationChange;
                _playerModel.OnDirectSpeedChange -= OnDirectSpeedChange;
                _playerModel.OnAttackAdditionalCountCdChange -= OnAttackAdditionalCountCdChange;
                _playerModel.OnAttackAdditionalCountChange -= OnAttackAdditionalCountChange;
                _playerModel.OnHpChange -= OnHpChange;
            }

            if (_gameModel != null)
            {
                _gameModel.OnScoreChange -= OnScoreChange;
            }
        }

        private void OnPositionChange(Vector2 pos)
        {
            coordsTT.text = $"Coords: {pos}";
        }

        private void OnRotationChange(float rotation)
        {
            angleTT.text = $"Angle: {rotation:F1}";
        }

        private void OnDirectSpeedChange(float speed)
        {
            speedTT.text = $"Speed: {speed:F1}";
        }

        private void OnHpChange(int hp)
        {
            hpTT.text = $"Hp: {hp}";
        }

        private void OnAttackAdditionalCountCdChange(float cd)
        {
            laserCdTT.text = $"Laser Cd: {cd:F1}";
        }

        private void OnAttackAdditionalCountChange(int count)
        {
            laserCountTT.text = $"Laser Count: {count}";
        }

        private void OnScoreChange(int score)
        {
            scoreTT.text = $"Score: {score}";
        }

        public void GameOver(int score)
        {
            gameOverWindow.Open(score);
        }
    }
}
