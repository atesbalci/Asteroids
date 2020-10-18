using Asteroids.Scripts.Game.Models.BoundedObjects;
using UnityEngine;
using Zenject;

namespace Asteroids.Game.Behaviours.BoundedObjects
{
    public class BoundedObjectBehaviour : BaseBehaviour, IBoundedObject
    {
        private IBoundedObjectsList _boundedObjectsList;
        
        public Vector2 Position
        {
            get => Transform.position;
            set => Transform.position = value;
        }

        [Inject]
        public void Initialize(IBoundedObjectsList boundedObjectsList)
        {
            _boundedObjectsList = boundedObjectsList;
        }

        private void OnEnable()
        {
            _boundedObjectsList.Add(this);
        }

        private void OnDisable()
        {
            _boundedObjectsList.Remove(this);
        }
    }
}