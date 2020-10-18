using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Scripts.Game.Models.Asteroids
{
    public class Asteroid
    {
        public readonly int Size;
        public readonly Vector2 Direction;
        public readonly float Velocity;

        public Asteroid(int size, Vector2 direction, float velocity)
        {
            Size = size;
            Direction = direction;
            Velocity = velocity;
        }

        public IList<Asteroid> Hit()
        {
            if (Size > 0)
            {
                var size = Size - 1;
                var retval =  new Asteroid[2];
                for (int i = 0; i < 2; i++)
                {
                    var angle = Random.Range(-GameRules.AsteroidSplitDirectionRandomization,
                        GameRules.AsteroidSplitDirectionRandomization);
                    retval[i] = new Asteroid(size, Quaternion.AngleAxis(angle, Vector3.back) * Direction, Velocity);
                }

                return retval;
            }

            return new Asteroid[0];
        }
    }
}