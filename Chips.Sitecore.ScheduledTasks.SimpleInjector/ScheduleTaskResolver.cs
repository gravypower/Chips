using System;
using Chips.DependencyInjection.SimpleInjector;

namespace Chips.Sitecore.ScheduledTasks.SimpleInjector
{
    public class ScheduleTaskResolver<TScheduleTask>: AbstractScheduleTaskResolver<TScheduleTask>
    where TScheduleTask : IScheduleTask
    {
        protected override IScheduleTask ResolveScheduleTask(Type scheduleTaskType)
        {
            return (IScheduleTask)SimpleInjectorBootstrapper.Container.GetInstance(scheduleTaskType);
        }
    }
}
