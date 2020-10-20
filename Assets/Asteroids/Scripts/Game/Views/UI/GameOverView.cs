using Asteroids.Scripts.Game.Models.GameState;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.Game.Views.UI
{
    public class GameOverView : MonoBehaviour
    {
        private GameStateData _gameStateData;
        
        [Inject]
        public void Initialize(GameStateData gameStateData)
        {
            _gameStateData = gameStateData;
            
            _gameStateData.GameOver += OnGameOver;
            _gameStateData.OnReset += OnOnReset;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _gameStateData.GameOver -= OnGameOver;
            _gameStateData.OnReset -= OnOnReset;
        }

        private void OnOnReset()
        {
            gameObject.SetActive(false);
        }

        private void OnGameOver()
        {
            gameObject.SetActive(true);
        }
    }
}