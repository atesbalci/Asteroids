using Asteroids.Game.Behaviours.Projectiles;
using Asteroids.Scripts.Game.Models;
using Asteroids.Scripts.Game.Models.Input;
using Asteroids.Scripts.Game.Models.Players;
using Asteroids.Scripts.Game.Models.Projectile;
using UnityEngine;
using Zenject;

namespace Asteroids.Game.Behaviours.Players
{
    public class PlayerBehaviour : PhysicalBehaviour, IHittable
    {
        private Player _player;
        private IGameInput _input;
        private float _lastFireTime;
        private ProjectileBehaviour.Pool _projectilePool;

        [Inject]
        public void Initialize(Player player, IGameInput input, ProjectileBehaviour.Pool projectilePool)
        {
            _player = player;
            _input = input;
            _lastFireTime = -GameRules.FireCooldown;
            _projectilePool = projectilePool;
        }

        private void Update()
        {
            if (_input.IsLeftPressed())
            {
                Turn(false);
            }
            
            if (_input.IsRightPressed())
            {
                Turn(true);
            }
            
            DampenVelocity();
            SetThrust(_input.IsForwardPressed());

            if (_input.IsFirePressed())
            {
                AttemptToFire();
            }
        }

        private void AttemptToFire()
        {
            var time = Time.time;
            if ((time - _lastFireTime) > GameRules.FireCooldown)
            {
                _lastFireTime = time;
                _projectilePool.Spawn().Fire(new Projectile(GameRules.ProjectileVelocity, Time.time, Transform.position,
                    Transform.up, GameRules.ProjectileMaxDistance, this));
            }
        }

        private void SetThrust(bool b)
        {
            _player.Thrusting = b;
            if (b)
            {
                Rigidbody.velocity += (Vector2) Transform.up * (Time.deltaTime * GameRules.ThrustPower);

                // Normalize...
                if (Rigidbody.velocity.sqrMagnitude > GameRules.MaxVelocitySqr)
                {
                    Rigidbody.velocity = Rigidbody.velocity.normalized * GameRules.MaxVelocity;
                }
            }
        }

        private void Turn(bool right)
        {
            Transform.localEulerAngles += new Vector3(0f, 0f, (right ? -1f : 1f) * Time.deltaTime * GameRules.TurnRate);
        }

        private void DampenVelocity()
        {
            Rigidbody.velocity = Vector2.MoveTowards(Rigidbody.velocity, Vector2.zero,
                GameRules.DampeningPower * Time.deltaTime);
        }

        public void Hit()
        {
            _player.Hit();
        }
    }
}
