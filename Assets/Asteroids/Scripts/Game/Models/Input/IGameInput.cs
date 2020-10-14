namespace Asteroids.Scripts.Game.Models.Input
{
    public interface IGameInput
    {
        bool IsLeftPressed();
        bool IsRightPressed();
        bool IsFirePressed();
        bool IsForwardPressed();
    }
}