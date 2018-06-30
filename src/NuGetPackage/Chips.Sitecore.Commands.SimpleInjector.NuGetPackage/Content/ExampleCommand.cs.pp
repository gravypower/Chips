using Sitecore.Data.Items;
using Sitecore.Tasks;

namespace $rootnamespace$
{
    public class ExampleCommand : ICommand
    {
        public void Execute(Item[] items, CommandItem commandItem, ScheduleItem schedule)
        {
            _logAdaptor.LogInfo($"Hello from ExampleCommand, called from {commandItem.Name}");
        }
    }
}