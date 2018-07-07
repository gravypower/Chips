using System;
using Chips.DependencyInjection.SimpleInjector;

namespace Chips.Sitecore.Factory
{
    public class FactoryContainerAdapter : AbstractFactoryContainerAdapter
    {
        protected override object ResolveType(Type type)=> SimpleInjectorBootstrapper.Container.GetInstance(type);
    }
}
