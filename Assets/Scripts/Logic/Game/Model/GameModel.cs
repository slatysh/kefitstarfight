using System;

namespace Logic.Game
{
    public class GameModel
    {
        private int _score;
        private event Action<int> _onScoreChange;

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                _onScoreChange?.Invoke(_score);
            }
        }

        public event Action<int> OnScoreChange
        {
            add => _onScoreChange += value;
            remove => _onScoreChange -= value;
        }
    }
}
