using Asteroids.Scripts.Game.Models.Asteroids;
using UnityEngine;

namespace Asteroids.Game.Behaviours.Asteroids.Managers
{
    public interface IAsteroidsManager
    {
        event AsteroidEvent AsteroidSpawned;
        event AsteroidEvent AsteroidDespawned;
        
        void Spawn(Asteroid asteroid, Vector2 position);
        void Despawn(AsteroidBehaviour asteroidBehaviour);
        void Clear();
        int SpawnedAsteroidCount { get; }
    }

    public delegate void AsteroidEvent(Asteroid asteroid);
}