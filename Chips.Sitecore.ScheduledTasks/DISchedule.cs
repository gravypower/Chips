using Sitecore.Data.Items;
using Sitecore.Tasks;

namespace Chips.Sitecore.ScheduledTasks
{
    public class DISchedule : ISchedule
    {
        public void Execute(Item[] items, CommandItem command, ScheduleItem schedule)
        {
            global::Sitecore.Diagnostics.Log.Info("My Sitecore scheduled task is being run!", this);
        }
    }

    public class DISchedule<TSchedule>
    where TSchedule : ISchedule
    {
        public void Execute(Item[] items, CommandItem command, ScheduleItem schedule)
        {
           // var schedule1 = (ISchedule) Bootstrapper.Container.GetInstance(typeof(TSchedule));
            //schedule1.Execute(items, command,schedule);
        }
    }
}