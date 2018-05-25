using System;
using Sitecore.Data.Items;
using Sitecore.Tasks;

namespace Chips.Sitecore.ScheduledTasks
{
    public abstract class AbstractScheduleTaskResolver<TScheduleTask>
    where TScheduleTask : IScheduleTask
    {
        public void Execute(Item[] items, CommandItem command, ScheduleItem schedule)
        {
            var scheduleTask = ResolveScheduleTask(typeof(TScheduleTask));
            scheduleTask.Execute(items, command,schedule);
        }

        protected abstract IScheduleTask ResolveScheduleTask(Type scheduleTaskType);
    }
}