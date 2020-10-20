using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Scripts.Game.Models
{
    public static class GameRules
    {
        public const int StartLives = 3;
        public const float TurnRate = 180f;
        public const float ThrustPower = 8f;
        public const float DampeningPower = 2f;
        public const float MaxVelocity = 4f;
        public const float MaxVelocitySqr = MaxVelocity * MaxVelocity;
        public const float BoundOffset = 1f;
        public const float ProjectileVelocity = 10f;
        public const float ProjectileMaxDistance = 6f;
        public const float FireCooldown = 0.4f;
        public const float AsteroidSplitDirectionRandomizationAngle = 60f;
        public const float AsteroidSpawnDistance = 5f;
        public const int AsteroidsPerSpawn = 4;
        public const float OpponentVelocity = 2f;
        public const float OpponentFireInterval = 3f;
        public const float OpponentSpawnMinDuration = 10f;
        public const float OpponentSpawnMaxDuration = 30f;
        public const int OpponentKillPoints = 200;
        public const float OpponentShootRandomizationAngle = 60f;

        public static readonly IList<OpponentSpawn> OpponentSpawns = new[]
        {
            new OpponentSpawn {NormalizedLocation = new Vector2(0f, 0.25f), FlightDirection = Vector2.right},
            new OpponentSpawn {NormalizedLocation = new Vector2(1f, 0.25f), FlightDirection = Vector2.left},
            new OpponentSpawn {NormalizedLocation = new Vector2(0f, 0.75f), FlightDirection = Vector2.right},
            new OpponentSpawn {NormalizedLocation = new Vector2(1f, 0.75f), FlightDirection = Vector2.left}
        };

        private const float MinAsteroidVelocity = 0.5f;
        private const float MaxAsteroidVelocity = 2f;

        private static readonly float[] AsteroidSizeScales = {0.35f, 0.7f, 1.4f};
        private static readonly int[] AsteroidSizePoints = {40, 30, 20};

        public static float GetSizeScale(int size)
        {
            return AsteroidSizeScales[Mathf.Clamp(size, 0, AsteroidSizeScales.Length - 1)];
        }
        
        public static int GetSizePoints(int size)
        {
            return AsteroidSizePoints[Mathf.Clamp(size, 0, AsteroidSizeScales.Length - 1)];
        }

        public static float GenerateAsteroidVelocity()
        {
            return Random.Range(MinAsteroidVelocity, MaxAsteroidVelocity);
        }
    }

    public struct OpponentSpawn
    {
        public Vector2 NormalizedLocation;
        public Vector2 FlightDirection;
    }
}