using System;
using Chips.DependencyInjection.SimpleInjector;

namespace Chips.Sitecore.Factory.Simpleinjector
{
    public class FactoryContainerAdapter: AbstractFactoryContainerAdapter
    {
        protected override object ResolveType(Type type)=> SimpleInjectorBootstrapper.Container.GetInstance(type);
    }
}
