using System;

namespace Asteroids.Scripts.Game.Models.GameState
{
    public class GameStateData
    {
        public event Action GameOver;
        
        public int Lives { get; private set; }

        public GameStateData()
        {
            Reset();
        }

        public void Reset()
        {
            Lives = 3;
        }

        public void DecrementLife()
        {
            Lives--;
            if (Lives <= 0)
            {
                GameOver?.Invoke();
            }
        }
    }
}