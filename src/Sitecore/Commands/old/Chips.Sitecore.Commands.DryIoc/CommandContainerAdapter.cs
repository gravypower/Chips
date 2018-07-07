using System;
using Chips.DependencyInjection.DryIoc;
using DryIoc;

namespace Chips.Sitecore.Commands
{
    public class CommandContainerAdapter : AbstractCommandContainerAdapter
    {
        protected override ICommand ResolveScheduleTask(Type commandType)
        {
            return (ICommand)DryIocBootstrapper.Container.Resolve(commandType);
        }
    }
}
