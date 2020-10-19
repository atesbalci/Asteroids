using System;

namespace Asteroids.Scripts.Game.Models.GameState
{
    public class GameStateData
    {
        public event Action GameOver;
        public event Action<int> LivesChanged;
        public event Action<long> ScoreChanged;
        
        private long _score;
        
        public int Lives { get; private set; }

        public long Score
        {
            get => _score;
            set
            {
                if (_score == value) return;
                _score = value;
                ScoreChanged?.Invoke(value);
            }
        }

        public GameStateData()
        {
            Reset();
        }

        public void Reset()
        {
            Lives = 3;
            Score = 0;
            LivesChanged?.Invoke(Lives);
        }

        public void DecrementLife()
        {
            Lives--;
            LivesChanged?.Invoke(Lives);
            if (Lives <= 0)
            {
                GameOver?.Invoke();
            }
        }
    }
}