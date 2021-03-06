﻿using UnityEngine;

namespace Asteroids.Game.Behaviours
{
    public abstract class BaseBehaviour : MonoBehaviour
    {
        public Transform Transform => _transform ? _transform : (_transform = transform);

        private Transform _transform;
    }
}