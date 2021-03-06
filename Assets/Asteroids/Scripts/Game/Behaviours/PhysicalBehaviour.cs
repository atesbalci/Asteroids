﻿using UnityEngine;

namespace Asteroids.Game.Behaviours
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PhysicalBehaviour : BaseBehaviour
    {
        private Collider _collider;
        private Rigidbody2D _rigidbody;

        protected Collider Collider => _collider ? _collider : (_collider = GetComponent<Collider>());
        protected Rigidbody2D Rigidbody => _rigidbody ? _rigidbody : (_rigidbody = GetComponent<Rigidbody2D>());
    }
}