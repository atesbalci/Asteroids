using System.Collections.Generic;

namespace Asteroids.Scripts.Game.Models.BoundedObjects
{
    public interface IBoundedObjectsList : IEnumerable<IBoundedObject>
    {
        void Add(IBoundedObject boundedObject);
        void Remove(IBoundedObject boundedObject);
    }
}