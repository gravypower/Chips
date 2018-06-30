using Chips.DependencyInjection.DryIoc;
using DryIoc;

namespace Chips.Sitecore.Commands.DryIoc
{
    public class CommandContainerAdapter<TCommand> : AbstractCommandContainerAdapter<TCommand>
    where TCommand : ICommand
    {
        protected override ICommand ResolveScheduleTask()
        {
            return (ICommand)DryIocBootstrapper.Container.Resolve(typeof(TCommand));
        }
    }
}
