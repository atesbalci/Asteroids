using System;

namespace Asteroids.Scripts.Game.Models.GameState
{
    public class GameStateData
    {
        public event Action GameOver;
        public event Action<int> LivesChanged;
        public event Action<long> ScoreChanged;
        public event Action OnReset;
        
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
            Lives = GameRules.StartLives;
            Score = 0;
            OnReset?.Invoke();
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