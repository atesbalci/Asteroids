using UnityEngine;

namespace Asteroids.Scripts.Game.Models
{
    public static class GameRules
    {
        public const float TurnRate = 180f;
        public const float ThrustPower = 8f;
        public const float DampeningPower = 2f;
        public const float MaxVelocity = 4f;
        public const float MaxVelocitySqr = MaxVelocity * MaxVelocity;
        public const float BoundOffset = 1f;
        public const float ProjectileVelocity = 10f;
        public const float ProjectileMaxDistance = 6f;
        public const float FireCooldown = 0.4f;
        public const float AsteroidSplitDirectionRandomization = 60f;

        private static readonly float[] AsteroidSizeScales = { 0.5f, 1f, 2f };

        public static float GetSizeScale(int size)
        {
            return AsteroidSizeScales[Mathf.Clamp(size, 0, AsteroidSizeScales.Length - 1)];
        }
    }
}