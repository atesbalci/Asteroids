﻿using Asteroids.Scripts.Game.Models;
using Asteroids.Scripts.Game.Models.BoundedObjects;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.Game.Controllers
{
    public class BoundariesController : IFixedTickable
    {
        private readonly IBoundedObjectsList _boundedObjectsList;
        private readonly Bounds _bounds;
        
        public BoundariesController(IBoundedObjectsList boundedObjectsList)
        {
            _boundedObjectsList = boundedObjectsList;

            var camera = Camera.main;
            if (camera != null)
            {
                var pos = camera.transform.position;
                pos.z = 0f;
                var size = camera.ViewportToWorldPoint(new Vector3(1f, 1f)) -
                           camera.ViewportToWorldPoint(new Vector3(-1f, -1f));
                size.z = 1f;
                _bounds = new Bounds(pos, size * 0.5f + new Vector3(GameRules.BoundOffset, GameRules.BoundOffset));
            }
        }
        
        public void FixedTick()
        {
            var bounds = _bounds;
            var max = bounds.max;
            var min = bounds.min;
            var size = bounds.size;
            foreach (var obj in _boundedObjectsList)
            {
                if (!bounds.Contains(obj.Position))
                {
                    var pos = obj.Position;
                    if (pos.x > max.x)
                    {
                        pos.x -= size.x;
                    }
                    else if (pos.x < min.x)
                    {
                        pos.x += size.x;
                    }

                    if (pos.y > max.y)
                    {
                        pos.y -= size.y;
                    }
                    else if (pos.y < min.y)
                    {
                        pos.y += size.y;
                    }

                    obj.Position = pos;
                }
            }
        }
    }
}