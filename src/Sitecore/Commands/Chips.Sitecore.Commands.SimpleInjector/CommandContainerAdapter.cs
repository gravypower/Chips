using Chips.DependencyInjection.SimpleInjector;

namespace Chips.Sitecore.Commands.SimpleInjector
{
    public class CommandContainerAdapter<TCommand> : AbstractCommandContainerAdapter<TCommand>
    where TCommand : ICommand
    {
        protected override ICommand ResolveScheduleTask()
        {
            return (ICommand)SimpleInjectorBootstrapper.Container.GetInstance(typeof(TCommand));
        }
    }
}
