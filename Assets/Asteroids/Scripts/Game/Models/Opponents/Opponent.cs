using System;
using UnityEngine;

namespace Asteroids.Scripts.Game.Models.Opponents
{
    public class Opponent
    {
        public event Action Death;
        
        public readonly Vector2 Direction;

        public Opponent(Vector2 direction)
        {
            Direction = direction;
        }

        public void Die()
        {
            Death?.Invoke();
        }
    }
}