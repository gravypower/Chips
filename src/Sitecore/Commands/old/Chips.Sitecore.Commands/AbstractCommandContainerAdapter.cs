using System;
using Sitecore.Data.Items;
using Sitecore.Tasks;

namespace Chips.Sitecore.Commands
{
    public abstract class AbstractCommandContainerAdapter
    {
        protected abstract ICommand ResolveScheduleTask(Type commandType);
        public void Execute(Item[] items, CommandItem commandItem, ScheduleItem schedule)
        {
            var commandTypecommand = commandItem["Command Type"];
            var commandType = Type.GetType(commandTypecommand);
            var command = (ICommand) ResolveScheduleTask(commandType);
            command.Execute(items, commandItem, schedule);
        }
    }
}