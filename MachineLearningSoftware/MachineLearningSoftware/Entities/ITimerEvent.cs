using System.Timers;

namespace MachineLearningSoftware.Entities
{
    public interface ITimerEvent
    {
        void InititateTimer(int interval, ElapsedEventHandler eventHandler);
    }
}
