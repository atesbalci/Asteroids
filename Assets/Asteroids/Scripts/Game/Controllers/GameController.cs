using System;
using Asteroids.Game.Behaviours.Asteroids;
using Asteroids.Game.Behaviours.Players;
using Asteroids.Scripts.Game.Models.Asteroids;
using Asteroids.Scripts.Game.Models.GameState;
using Asteroids.Scripts.Game.Models.Players;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Asteroids.Scripts.Game.Controllers
{
    public class GameController : ITickable, IDisposable
    {
        private readonly AsteroidBehaviour.Pool _asteroidPool;
        private readonly Player _player;
        private readonly GameStateData _gameStateData;
        private readonly PlayerBehaviour _playerBehaviour;

        private float _timeOfLastSpawn;

        public GameController(AsteroidBehaviour.Pool asteroidPool, Player player, GameStateData gameStateData,
            PlayerBehaviour playerBehaviour)
        {
            _asteroidPool = asteroidPool;
            _player = player;
            _gameStateData = gameStateData;
            _playerBehaviour = playerBehaviour;

            _player.Death += OnDeath;
            
            Respawn();
        }

        public void Dispose()
        {
            _player.Death -= OnDeath;
        }

        public void Tick()
        {
            var time = Time.time;
            if (time - _timeOfLastSpawn > 5f)
            {
                _timeOfLastSpawn = time;
                _asteroidPool.Spawn().Bind(new Asteroid(2, Random.insideUnitCircle.normalized, 2f), Vector2.zero);
            } 
        }

        private void OnDeath()
        {
            _gameStateData.DecrementLife();
            Respawn();
        }

        private void Respawn()
        {
            _playerBehaviour.Transform.position = Vector3.zero;
            _playerBehaviour.Transform.rotation = Quaternion.identity;
        }
    }
}