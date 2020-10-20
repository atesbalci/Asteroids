using Asteroids.Game.Behaviours.Asteroids.Managers;
using Asteroids.Game.Behaviours.Opponents;
using Asteroids.Game.Behaviours.Players;
using Asteroids.Helpers.Bounds;
using Asteroids.Helpers.Timing;
using Asteroids.Scripts.Game.Controllers;
using Asteroids.Scripts.Game.Models;
using Asteroids.Scripts.Game.Models.GameState;
using Asteroids.Scripts.Game.Models.Input;
using Asteroids.Scripts.Game.Models.Players;
using NSubstitute;
using NUnit.Framework;

namespace Asteroids.Tests.Controllers
{
    [TestFixture]
    public class GameControllerTests
    {
        private GameController _gameController;
        private Player _player;
        private GameStateData _gameStateData;

        [SetUp]
        public void SetUp()
        {
            _player = new Player();
            _gameStateData = new GameStateData();
            _gameController = new GameController(
                Substitute.For<IAsteroidsManager>(),
                _player,
                _gameStateData,
                Substitute.For<IPlayerBehaviour>(),
                Substitute.For<ITimingManager>(),
                Substitute.For<IGameInput>(),
                Substitute.For<IOpponentBehaviour>(),
                Substitute.For<IBoundProvider>());
        }

        [Test]
        public void TestLives()
        {
            Assert.AreEqual(GameRules.StartLives, _gameStateData.Lives);
            _player.Hit();
            Assert.AreEqual(GameRules.StartLives - 1, _gameStateData.Lives);
        }

        [Test]
        public void TestGameOver()
        {
            bool isGameOver = false;
            _gameStateData.GameOver += () => isGameOver = true;
            for (int i = 0; i < GameRules.StartLives; i++)
            {
                _player.Hit();
            }
            
            Assert.IsTrue(isGameOver);
        }
    }
}