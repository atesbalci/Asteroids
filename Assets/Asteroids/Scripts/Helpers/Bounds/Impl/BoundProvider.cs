using UnityEngine;

namespace Asteroids.Helpers.Bounds.Impl
{
    public class BoundProvider : IBoundProvider
    {
        private Camera _camera;
        
        private Camera Camera => _camera ? _camera : (_camera = Camera.main);
        
        public Vector2 GetWorldPosition(Vector2 normalizedPosition)
        {
            return Camera.ViewportToWorldPoint(normalizedPosition);
        }
    }
}