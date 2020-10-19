using Asteroids.Game.Behaviours.Asteroids.Managers;
using Asteroids.Scripts.Game.Models;
using Asteroids.Scripts.Game.Models.Asteroids;
using Asteroids.Scripts.Game.Models.Projectile;
using UnityEngine;
using Zenject;

namespace Asteroids.Game.Behaviours.Asteroids
{
    public class AsteroidBehaviour : PhysicalBehaviour, IHittable
    {
        private IAsteroidsManager _asteroidsManager;
        
        public Asteroid Asteroid { get; private set; }

        [Inject]
        public void Initialize(IAsteroidsManager asteroidsManager)
        {
            _asteroidsManager = asteroidsManager;
        }
        
        public void Bind(Asteroid asteroid, Vector2 origin)
        {
            Asteroid = asteroid;
            Transform.position = origin;
            Rigidbody.velocity = Asteroid.Velocity * Asteroid.Direction;
            Transform.localScale = Vector3.one * GameRules.GetSizeScale(Asteroid.Size);
        }

        public void Die()
        {
            _asteroidsManager.Despawn(this);
        }
        
        public void Hit()
        {
            Die();
            foreach (var asteroid in Asteroid.Hit())
            {
                _asteroidsManager.Spawn(asteroid, Transform.position);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            other.gameObject.GetComponent<IHittable>()?.Hit();
        }

        public class Pool : MonoMemoryPool<AsteroidBehaviour> { }
    }
}