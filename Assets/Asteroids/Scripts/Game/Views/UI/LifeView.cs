using Asteroids.Scripts.Game.Models.GameState;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.Game.Views.UI
{
    public class LifeView : MonoBehaviour
    {
        [SerializeField] private GameObject[] _lifeReps;

        private GameStateData _gameStateData;

        [Inject]
        public void Initialize(GameStateData gameStateData)
        {
            _gameStateData = gameStateData;
            _gameStateData.LivesChanged += OnLivesChanged;
            OnLivesChanged(_gameStateData.Lives);
        }

        private void OnDestroy()
        {
            _gameStateData.LivesChanged -= OnLivesChanged;
        }

        private void OnLivesChanged(int lives)
        {
            for (int i = 0; i < _lifeReps.Length; i++)
            {
                _lifeReps[i].SetActive(i < lives);
            }
        }
    }
}