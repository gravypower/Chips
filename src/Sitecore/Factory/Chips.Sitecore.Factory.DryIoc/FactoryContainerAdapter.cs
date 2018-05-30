using System;
using Chips.DependencyInjection.DryIoc;
using DryIoc;

namespace Chips.Sitecore.Factory.DryIoc
{
    public class FactoryContainerAdapter: AbstractFactoryContainerAdapter
    {
        protected override object ResolveType(Type type) => DryIocBootstrapper.Container.Resolve(type);
    }
}
