using Asteroids.Scripts.Game.Models.Projectile;
using UnityEngine;
using Zenject;

namespace Asteroids.Game.Behaviours.Projectiles
{
    public class ProjectileBehaviour : PhysicalBehaviour
    {
        private Pool _pool;
        private Projectile _projectile;
        private bool _dead;

        [Inject]
        public void Initialize(Pool pool)
        {
            _pool = pool;
        }
        
        public void Fire(Projectile projectile)
        {
            _dead = false;
            _projectile = projectile;
            Transform.position = projectile.Origin;
            Rigidbody.velocity = projectile.Direction * projectile.Velocity;
        }

        private void FixedUpdate()
        {
            if ((Time.time - _projectile.SpawnTime) * _projectile.Velocity > _projectile.MaxDistance)
            {
                Die();
            }
        }

        private void Die()
        {
            if (!_dead)
            {
                _pool.Despawn(this);
                _dead = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var hittable = other.GetComponent<IHittable>();
            if (hittable != null && _projectile.Source != hittable)
            {
                hittable.Hit();
                Die();
            }
        }

        public class Pool : MonoMemoryPool<ProjectileBehaviour> { }
    }
}