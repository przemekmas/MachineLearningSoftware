using System.Timers;

namespace MachineLearningSoftware.Entities
{
    class TimerEvent : ITimerEvent
    {
        public void InititateTimer(int interval, ElapsedEventHandler eventHandler)
        {
            var timer = new Timer(interval);
            timer.Elapsed += eventHandler;
            timer.Start();
        }
    }
}
