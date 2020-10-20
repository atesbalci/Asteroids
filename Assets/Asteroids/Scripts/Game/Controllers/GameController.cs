using System;
using Asteroids.Game.Behaviours.Asteroids.Managers;
using Asteroids.Game.Behaviours.Opponents;
using Asteroids.Game.Behaviours.Players;
using Asteroids.Helpers.Bounds;
using Asteroids.Helpers.Collections;
using Asteroids.Helpers.Timing;
using Asteroids.Scripts.Game.Models;
using Asteroids.Scripts.Game.Models.Asteroids;
using Asteroids.Scripts.Game.Models.GameState;
using Asteroids.Scripts.Game.Models.Input;
using Asteroids.Scripts.Game.Models.Opponents;
using Asteroids.Scripts.Game.Models.Players;
using Random = UnityEngine.Random;

namespace Asteroids.Scripts.Game.Controllers
{
    public class GameController : IDisposable
    {
        private readonly IAsteroidsManager _asteroidsManager;
        private readonly Player _player;
        private readonly GameStateData _gameStateData;
        private readonly IPlayerBehaviour _playerBehaviour;
        private readonly ITimingManager _timingManager;
        private readonly IGameInput _gameInput;
        private readonly IOpponentBehaviour _opponentBehaviour;
        private readonly IBoundProvider _boundProvider;

        private IDisposable _opponentSpawnDisposable;

        public GameController(IAsteroidsManager asteroidsManager, Player player, GameStateData gameStateData,
            IPlayerBehaviour playerBehaviour, ITimingManager timingManager, IGameInput gameInput,
            IOpponentBehaviour opponentBehaviour, IBoundProvider boundProvider)
        {
            _asteroidsManager = asteroidsManager;
            _player = player;
            _gameStateData = gameStateData;
            _playerBehaviour = playerBehaviour;
            _timingManager = timingManager;
            _gameInput = gameInput;
            _opponentBehaviour = opponentBehaviour;
            _boundProvider = boundProvider;

            _player.Death += OnDeath;
            _asteroidsManager.AsteroidDespawned += OnAsteroidDespawn;
            _gameInput.RestartPressed += OnRestartPressed;

            StartGame();
        }

        public void Dispose()
        {
            _player.Death -= OnDeath;
            _asteroidsManager.AsteroidDespawned -= OnAsteroidDespawn;
            _gameInput.RestartPressed -= OnRestartPressed;
            _opponentSpawnDisposable?.Dispose();
        }

        private void OnRestartPressed()
        {
            StartGame();
        }

        private void OnAsteroidDespawn(Asteroid asteroid)
        {
            if (_gameStateData.Lives <= 0) return;
            _gameStateData.Score += GameRules.GetSizePoints(asteroid.Size);
            if (_asteroidsManager.SpawnedAsteroidCount <= 0)
            {
                SpawnAsteroids();
            }
        }

        private void StartGame()
        {
            _gameStateData.Reset();
            _asteroidsManager.Clear();
            _opponentBehaviour.Deactivate();
            Respawn();
            SpawnAsteroids();
            SetUpNextOpponentSpawn();
        }

        private void SetUpNextOpponentSpawn()
        {
            _opponentSpawnDisposable?.Dispose();
            _opponentSpawnDisposable = _timingManager.Delay(
                TimeSpan.FromSeconds(Random.Range(GameRules.OpponentSpawnMinDuration,
                    GameRules.OpponentSpawnMaxDuration)),
                () =>
                {
                    var spawn = GameRules.OpponentSpawns.GetRandom();
                    var opponent = new Opponent(spawn.FlightDirection);
                    _opponentBehaviour.Bind(opponent, _boundProvider.GetWorldPosition(spawn.NormalizedLocation));
                    opponent.Death += OnOpponentDeath;
                });
        }

        private void OnOpponentDeath()
        {
            SetUpNextOpponentSpawn();
            if (_gameStateData.Lives > 0)
            {
                _gameStateData.Score += GameRules.OpponentKillPoints;
            }
        }

        private void SpawnAsteroid()
        {
            var position = _playerBehaviour.Position +
                           Random.insideUnitCircle.normalized * GameRules.AsteroidSpawnDistance;
            var direction = Random.insideUnitCircle.normalized;
            _asteroidsManager.Spawn(
                new Asteroid(2, direction, GameRules.GenerateAsteroidVelocity()),
                position);
        }

        private void SpawnAsteroids()
        {
            for (int i = 0; i < GameRules.AsteroidsPerSpawn; i++)
            {
                SpawnAsteroid();
            }
        }

        private void OnDeath()
        {
            _gameStateData.DecrementLife();
            _playerBehaviour.SetActive(false);
            if (_gameStateData.Lives > 0)
            {
                _timingManager.Delay(TimeSpan.FromSeconds(1), Respawn);
            }
        }

        private void Respawn()
        {
            _playerBehaviour.SetActive(true);
            _playerBehaviour.ResetToSpawn();
        }
    }
}