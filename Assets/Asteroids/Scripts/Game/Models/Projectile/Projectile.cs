using UnityEngine;

namespace Asteroids.Scripts.Game.Models.Projectile
{
    public class Projectile
    {
        public readonly float Velocity;
        public readonly float SpawnTime;
        public readonly Vector2 Origin;
        public readonly Vector2 Direction;
        public readonly float MaxDistance;
        public readonly IHittable Source;

        public Projectile(float velocity, float spawnTime, Vector2 origin, Vector2 direction, float maxDistance,
            IHittable source = null)
        {
            Velocity = velocity;
            SpawnTime = spawnTime;
            Origin = origin;
            Direction = direction;
            MaxDistance = maxDistance;
            Source = source;
        }
    }
}