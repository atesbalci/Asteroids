using Asteroids.Scripts.Game.Models.BoundedObjects;
using Zenject;

namespace Asteroids.Scripts.Game.Controllers
{
    public class BoundariesController : IFixedTickable
    {
        private readonly BoundaryData _boundaryData;
        
        public BoundariesController(BoundaryData boundaryData)
        {
            _boundaryData = boundaryData;
        }
        
        public void FixedTick()
        {
            foreach (var obj in _boundaryData)
            {
                
            }
        }
    }
}