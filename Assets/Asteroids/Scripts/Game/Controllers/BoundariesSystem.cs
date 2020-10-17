using Asteroids.Game.Behaviours.Components;
using Asteroids.Scripts.Game.Models;
using Unity.Entities;
using UnityEngine;

namespace Asteroids.Scripts.Game.Controllers
{
    public class BoundariesSystem : ComponentSystem
    {
        private EntityQuery _entityQuery;
        private Bounds _bounds;

        protected override void OnCreate()
        {
            base.OnCreate();
            _entityQuery = GetEntityQuery(typeof(Transform), ComponentType.ReadOnly<BoundedObject>());
            RequireForUpdate(_entityQuery);
        }

        protected override void OnStartRunning()
        {
            base.OnStartRunning();
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

        protected override void OnUpdate()
        {
            var max = _bounds.max;
            var min = _bounds.min;
            var size = _bounds.size;
            Entities.With(_entityQuery).ForEach((Transform transform, ref BoundedObject unused) =>
            {
                var pos = transform.position;
                if (!_bounds.Contains(pos))
                {
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

                    transform.position = pos;
                }
            });
        }
    }
}