﻿using Chips.DependencyInjection.DryIoc;
using DryIoc;

namespace Chips.Sitecore.ScheduledTasks.Dryioc
{
    public class ScheduleTaskContainerAdapter<TScheduleTask>: AbstractScheduleTaskContainerAdapter<TScheduleTask>
    where TScheduleTask : IScheduleTask
    {
        protected override IScheduleTask ResolveScheduleTask()
        {
            return (IScheduleTask)DryIocBootstrapper.Container.Resolve(typeof(TScheduleTask));
        }
    }
}