using Chips.DependencyInjection.SimpleInjector;

namespace Chips.Sitecore.ScheduledTasks.SimpleInjector
{
    public class ScheduleTaskContainerAdapter<TScheduleTask>: AbstractScheduleTaskContainerAdapter<TScheduleTask>
    where TScheduleTask : IScheduleTask
    {
        protected override IScheduleTask ResolveScheduleTask()
        {
            return (IScheduleTask)SimpleInjectorBootstrapper.Container.GetInstance(typeof(TScheduleTask));
        }
    }
}
