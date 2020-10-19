using System.Collections.Generic;
using Asteroids.Scripts.Game.Models.Asteroids;
using UnityEngine;
using Zenject;

namespace Asteroids.Game.Behaviours.Asteroids.Managers.Impl
{
    public class PoolAsteroidsManager : IAsteroidsManager, IInitializable
    {
        public event AsteroidEvent AsteroidSpawned;
        public event AsteroidEvent AsteroidDespawned;

        private readonly AsteroidBehaviour.Pool _asteroidsPool;
        private readonly ICollection<AsteroidBehaviour> _spawnedAsteroids;
        
        public PoolAsteroidsManager(AsteroidBehaviour.Pool asteroidsPool)
        {
            _asteroidsPool = asteroidsPool;
            _spawnedAsteroids = new LinkedList<AsteroidBehaviour>();
        }

        public void Initialize()
        {
            _asteroidsPool.Resize(30);
        }

        public void Spawn(Asteroid asteroid, Vector2 position)
        {
            var behaviour = _asteroidsPool.Spawn();
            behaviour.Bind(asteroid, position);
            _spawnedAsteroids.Add(behaviour);
            AsteroidSpawned?.Invoke(asteroid);
        }

        public void Despawn(AsteroidBehaviour asteroidBehaviour)
        {
            _spawnedAsteroids.Remove(asteroidBehaviour);
            _asteroidsPool.Despawn(asteroidBehaviour);
            AsteroidDespawned?.Invoke(asteroidBehaviour.Asteroid);
        }

        public void Clear()
        {
            foreach (var asteroidBehaviour in _spawnedAsteroids)
            {
                _asteroidsPool.Despawn(asteroidBehaviour);
            }
            
            _spawnedAsteroids.Clear();
        }

        public int SpawnedAsteroidCount => _spawnedAsteroids.Count;
    }
}