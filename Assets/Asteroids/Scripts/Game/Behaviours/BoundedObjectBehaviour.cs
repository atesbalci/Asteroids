using Asteroids.Game.Behaviours.Components;
using Unity.Entities;
using UnityEngine;

namespace Asteroids.Game.Behaviours
{
    [RequireComponent(typeof(GameObjectEntity))]
    public class BoundedObjectBehaviour : MonoBehaviour
    {
        private void Start()
        {
            var entity = GetComponent<GameObjectEntity>();
            entity.EntityManager.AddComponent<BoundedObject>(entity.Entity);
        }
    }
}