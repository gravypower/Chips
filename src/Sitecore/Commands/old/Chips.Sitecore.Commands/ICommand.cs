using Sitecore.Data.Items;
using Sitecore.Tasks;

namespace Chips.Sitecore.Commands
{
    public interface ICommand
    {
        void Execute(Item[] items, CommandItem command, ScheduleItem schedule);
    }
}
