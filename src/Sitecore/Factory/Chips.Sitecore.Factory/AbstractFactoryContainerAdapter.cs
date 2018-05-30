using System;
using Sitecore.Reflection;

namespace Chips.Sitecore.Factory
{
    public abstract class AbstractFactoryContainerAdapter : IFactory
    {
        public object GetObject(string identifier)
        {
            var type = Type.GetType(identifier);
            return ResolveType(type);
        }
        protected abstract object ResolveType(Type type);
    }
}
