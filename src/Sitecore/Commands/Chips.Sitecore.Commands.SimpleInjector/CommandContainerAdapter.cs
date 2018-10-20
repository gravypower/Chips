using System;
using Chips.DependencyInjection.SimpleInjector;

namespace Chips.Sitecore.Commands
{
    public class CommandContainerAdapter : AbstractCommandContainerAdapter
    {
        protected override ICommand ResolveCommand(Type commandType)
        {
            return (ICommand)SimpleInjectorBootstrapper.Container.GetInstance(commandType);
        }
    }
}
