using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Chips.DependencyInjection
{
    public abstract class Bootstrapper<TContainer>
    {
        private readonly IEnumerable<Assembly> _assembls;
        protected abstract TContainer CreateContainer();
        protected abstract void VerifyContainer();

        public static TContainer Container { get; protected set; }

        protected Bootstrapper(IEnumerable<Assembly> assembls)
        {
            _assembls = assembls;
        }

        public void Bootstrap()
        {
            Container = CreateContainer();

            foreach (var bootstrapType in GetBootstrapTypes())
            {
                var bootstrap = (IBootstrap<TContainer>) Activator.CreateInstance(bootstrapType);
                bootstrap.Bootstrap(Container);
            }

            VerifyContainer();
        }

        private IEnumerable<Type> GetBootstrapTypes() =>
            from assembly in _assembls
            from type in assembly.GetExportedTypes()
            where typeof(IBootstrap<TContainer>).IsAssignableFrom(type) && !type.IsAbstract
            select type;
    }
}