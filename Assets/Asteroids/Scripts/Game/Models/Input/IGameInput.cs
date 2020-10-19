using System;

namespace Asteroids.Scripts.Game.Models.Input
{
    public interface IGameInput
    {
        event Action RestartPressed;

        bool IsLeftPressed();
        bool IsRightPressed();
        bool IsFirePressed();
        bool IsForwardPressed();
    }
}