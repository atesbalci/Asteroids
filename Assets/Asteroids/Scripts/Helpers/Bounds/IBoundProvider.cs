using UnityEngine;

namespace Asteroids.Helpers.Bounds
{
    public interface IBoundProvider
    {
        Vector2 GetWorldPosition(Vector2 normalizedPosition);
    }
}