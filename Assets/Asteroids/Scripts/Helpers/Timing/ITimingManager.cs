using System;

namespace Asteroids.Helpers.Timing
{
    public interface ITimingManager
    {
        IDisposable Delay(TimeSpan delay, Action action);
        IDisposable Interval(TimeSpan interval, Action action);
    }
}