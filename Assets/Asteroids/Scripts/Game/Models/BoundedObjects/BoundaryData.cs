using System.Collections;
using System.Collections.Generic;

namespace Asteroids.Scripts.Game.Models.BoundedObjects
{
    public class BoundaryData : IEnumerable<IBoundedObject>
    {
        private readonly ICollection<IBoundedObject> _objects;

        public BoundaryData()
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