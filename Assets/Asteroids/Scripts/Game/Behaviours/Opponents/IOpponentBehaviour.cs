using Asteroids.Scripts.Game.Models.Opponents;
using UnityEngine;

namespace Asteroids.Game.Behaviours.Opponents
{
    public interface IOpponentBehaviour
    {
        void Bind(Opponent opponent, Vector2 position);
        void Deactivate();
    }
}