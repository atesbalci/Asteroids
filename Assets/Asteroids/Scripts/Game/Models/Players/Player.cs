using System;

namespace Asteroids.Scripts.Game.Models.Players
{
    public class Player
    {
        public event Action Death;
        
        public bool Thrusting { get; set; }

        public void Hit()
        {
            Death?.Invoke();
        }
    }
}