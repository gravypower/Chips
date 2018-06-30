using Sitecore.Data.Items;
using Sitecore.Tasks;

namespace Chips.Sitecore.Commands
{
    public abstract class AbstractCommandContainerAdapter<TCommand>
    where TCommand : ICommand
    {
        public void Execute(Item[] items, CommandItem command, ScheduleItem schedule)
        {
            var scheduleTask = ResolveScheduleTask();
            scheduleTask.Execute(items, command,schedule);
        }

        protected abstract ICommand ResolveScheduleTask();
    }
}