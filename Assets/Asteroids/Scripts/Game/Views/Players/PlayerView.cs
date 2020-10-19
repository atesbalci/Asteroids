using Asteroids.Scripts.Game.Models.Players;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.Game.Views.Players
{
    public class PlayerView : MonoBehaviour
    {
        private const float FlickerRate = 20f;

        [SerializeField] private GameObject _thrusterEmissionObject;
        
        private Player _player;

        [Inject]
        public void Initialize(Player player)
        {
            _player = player;
        }

        private void Update()
        {
            _thrusterEmissionObject.SetActive(_player.Thrusting && Mathf.FloorToInt(Time.time * FlickerRate) % 2 == 0);
        }
    }
}