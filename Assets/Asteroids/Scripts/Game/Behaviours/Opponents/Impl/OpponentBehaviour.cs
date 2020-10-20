using System;
using Asteroids.Game.Behaviours.Players;
using Asteroids.Game.Behaviours.Projectiles;
using Asteroids.Helpers.Timing;
using Asteroids.Scripts.Game.Models;
using Asteroids.Scripts.Game.Models.Opponents;
using Asteroids.Scripts.Game.Models.Projectile;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Asteroids.Game.Behaviours.Opponents.Impl
{
    public class OpponentBehaviour : PhysicalBehaviour, IOpponentBehaviour, IHittable
    {
        private IPlayerBehaviour _playerBehaviour;
        private ITimingManager _timingManager;
        private ProjectileBehaviour.Pool _projectilePool;
        private Opponent _opponent;
        private IDisposable _fireDisposable;

        [Inject]
        public void Initialize(IPlayerBehaviour playerBehaviour, ITimingManager timingManager,
            ProjectileBehaviour.Pool projectilePool)
        {
            _playerBehaviour = playerBehaviour;
            _timingManager = timingManager;
            _projectilePool = projectilePool;
            if (_opponent == null)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            _fireDisposable = _timingManager.Interval(TimeSpan.FromSeconds(GameRules.OpponentFireInterval), Fire);
        }

        private void OnDisable()
        {
            _fireDisposable?.Dispose();
        }

        public void Bind(Opponent opponent, Vector2 position)
        {
            gameObject.SetActive(true);
            _opponent = opponent;
            Transform.position = position;
            Rigidbody.velocity = opponent.Direction * GameRules.OpponentVelocity;
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void Hit()
        {
            gameObject.SetActive(false);
            _opponent.Die();
        }

        private void Fire()
        {
            Vector2 pos = Transform.position;
            var direction =
                Quaternion.AngleAxis(
                    Random.Range(-GameRules.OpponentShootRandomizationAngle, GameRules.OpponentShootRandomizationAngle),
                    Vector3.back) * (_playerBehaviour.Position - pos).normalized;
            _projectilePool.Spawn().Fire(new Projectile(GameRules.ProjectileVelocity, Time.time, pos,
                direction, GameRules.ProjectileMaxDistance, this));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var player = other.gameObject.GetComponent<IPlayerBehaviour>();
            if (player != null)
            {
                Hit();
                (player as IHittable)?.Hit();
            }
        }
    }
}