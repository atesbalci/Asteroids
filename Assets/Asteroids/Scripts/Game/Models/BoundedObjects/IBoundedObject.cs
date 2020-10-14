using UnityEngine;

namespace Asteroids.Scripts.Game.Models.BoundedObjects
{
    public interface IBoundedObject
    {
        Vector2 Position { get; set; }
    }
}