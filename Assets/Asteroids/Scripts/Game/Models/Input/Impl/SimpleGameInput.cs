using System;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.Game.Models.Input.Impl
{
    using Input = UnityEngine.Input;
    
    public class SimpleGameInput : IGameInput, ITickable
    {
        public event Action RestartPressed;
        
        public bool IsLeftPressed()
        {
            return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        }

        public bool IsRightPressed()
        {
            return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        }

        public bool IsFirePressed()
        {
            return Input.GetKey(KeyCode.Space);
        }

        public bool IsForwardPressed()
        {
            return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartPressed?.Invoke();
            }
        }
    }
}