using Sitecore.Data.Items;
using Sitecore.Tasks;

namespace Chips.Sitecore.ScheduledTasks
{
    public interface ISchedule
    {
        void Execute(Item[] items, CommandItem command, ScheduleItem schedule);
    }
}
