using System.Collections;
using System.Collections.Generic;

namespace Asteroids.Scripts.Game.Models.BoundedObjects
{
    public class BoundedObjectsData : IBoundedObjectsList
    {
        private readonly ICollection<IBoundedObject> _objects;

        public BoundedObjectsData()
        {
            _objects = new LinkedList<IBoundedObject>();
        }

        public void Add(IBoundedObject obj)
        {
            _objects.Add(obj);
        }

        public void Remove(IBoundedObject obj)
        {
            _objects.Remove(obj);
        }

        public IEnumerator<IBoundedObject> GetEnumerator() => _objects.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}