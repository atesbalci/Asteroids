using Asteroids.Scripts.Game.Models;
using Asteroids.Scripts.Game.Models.Input;
using Asteroids.Scripts.Game.Models.Players;
using UnityEngine;
using Zenject;

namespace Asteroids.Game.Behaviours.Players
{
    public class PlayerBehaviour : PhysicalBehaviour
    {
        private Player _player;
        private IGameInput _input;

        public void Bind(Player player)
        {
            _player = player;
        }

        [Inject]
        public void Initialize(IGameInput input)
        {
            _input = input;
        }

        private void Update()
        {
            if (_input.IsLeftPressed())
            {
                Transform.localEulerAngles += new Vector3(0f, 0f, Time.deltaTime * GameRules.TurnRate);
            }
            
            if (_input.IsRightPressed())
            {
                Transform.localEulerAngles += new Vector3(0f, 0f, -Time.deltaTime * GameRules.TurnRate);
            }

            var velocity = Rigidbody.velocity;
            velocity = Vector2.MoveTowards(velocity, Vector2.zero,
                GameRules.DampeningPower * Time.deltaTime);

            if (_input.IsForwardPressed())
            {
                velocity += (Vector2) Transform.up * (Time.deltaTime * GameRules.ThrustPower);
            }

            if (velocity.sqrMagnitude > GameRules.MaxVelocitySqr)
            {
                velocity = velocity.normalized * GameRules.MaxVelocity;
            }

            Rigidbody.velocity = velocity;
        }
    }
}
