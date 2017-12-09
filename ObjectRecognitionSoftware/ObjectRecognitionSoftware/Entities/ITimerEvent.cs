using System.Timers;

namespace ObjectRecognitionSoftware.Entities
{
    public interface ITimerEvent
    {
        void InititateTimer(int interval, ElapsedEventHandler eventHandler);
    }
}
