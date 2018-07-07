using Chips.Sitecore.Commands;
using Sitecore.Abstractions;
using Sitecore.Data.Items;
using Sitecore.Tasks;

namespace $rootnamespace$
{
    public class ExampleCommand : ICommand
    {
        private readonly BaseLog _log;

        public ExampleCommand(BaseLog log)
        {
            _log = log;
        }

        public void Execute(Item[] items, CommandItem commandItem, ScheduleItem schedule)
        {
            _log.Info($"Hello from ExampleCommand, called from {commandItem.Name}", this);
        }
    }
}