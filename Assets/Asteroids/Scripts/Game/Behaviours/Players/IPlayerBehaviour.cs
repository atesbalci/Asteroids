using UnityEngine;

namespace Asteroids.Game.Behaviours.Players
{
    public interface IPlayerBehaviour
    {
        void ResetToSpawn();
        void SetActive(bool b);
        Vector2 Position { get; }
    }
}