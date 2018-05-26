using Sitecore.Data.Items;
using Sitecore.Tasks;

namespace Chips.Sitecore.ScheduledTasks
{
    public abstract class AbstractScheduleTaskContainerAdapter<TScheduleTask>
    where TScheduleTask : IScheduleTask
    {
        public void Execute(Item[] items, CommandItem command, ScheduleItem schedule)
        {
            var scheduleTask = ResolveScheduleTask();
            scheduleTask.Execute(items, command,schedule);
        }

        protected abstract IScheduleTask ResolveScheduleTask();
    }
}