using Asteroids.Scripts.Game.Models;
using Asteroids.Scripts.Game.Models.Asteroids;
using Asteroids.Scripts.Game.Models.Projectile;
using UnityEngine;
using Zenject;

namespace Asteroids.Game.Behaviours.Asteroids
{
    public class AsteroidBehaviour : PhysicalBehaviour, IHittable
    {
        private Pool _pool;
        private Asteroid _asteroid;

        [Inject]
        public void Initialize(Pool pool)
        {
            _pool = pool;
        }
        
        public void Bind(Asteroid asteroid, Vector2 origin)
        {
            _asteroid = asteroid;
            Transform.position = origin;
            Rigidbody.velocity = _asteroid.Velocity * _asteroid.Direction;
            Transform.localScale = Vector3.one * GameRules.GetSizeScale(_asteroid.Size);
        }

        public void Die()
        {
            _pool.Despawn(this);
        }
        
        public void Hit()
        {
            Die();
            foreach (var asteroid in _asteroid.Hit())
            {
                _pool.Spawn().Bind(asteroid, Transform.position);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            other.gameObject.GetComponent<IHittable>()?.Hit();
        }

        public class Pool : MonoMemoryPool<AsteroidBehaviour> { }
    }
}