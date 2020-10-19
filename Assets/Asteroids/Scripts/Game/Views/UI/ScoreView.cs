using Asteroids.Scripts.Game.Models.GameState;
using TMPro;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.Game.Views.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        private GameStateData _gameStateData;

        [Inject]
        public void Initialize(GameStateData gameStateData)
        {
            _gameStateData = gameStateData;
            _gameStateData.ScoreChanged += OnScoreChanged;
            OnScoreChanged(_gameStateData.Score);
        }

        private void OnDestroy()
        {
            _gameStateData.ScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(long score)
        {
            _scoreText.text = score.ToString();
        }
    }
}