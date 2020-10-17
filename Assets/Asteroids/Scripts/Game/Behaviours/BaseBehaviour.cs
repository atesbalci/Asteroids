using Asteroids.Scripts.Game.Models.BoundedObjects;
using UnityEngine;
using Zenject;

namespace Asteroids.Game.Behaviours
{
    public abstract class BaseBehaviour : MonoBehaviour, IBoundedObject
    {
        protected Transform Transform => _transform ? _transform : (_transform = transform);

        private Transform _transform;
        
        public Vector2 Position
        {
            get => Transform.position;
            set => Transform.position = value;
        }

        [Inject]
        public void Initialize(BoundaryData boundaryData)
        {
            boundaryData.Add(this);
        }
    }
}