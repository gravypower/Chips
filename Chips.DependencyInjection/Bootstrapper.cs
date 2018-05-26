using System;
using System.Collections.Generic;
using System.Linq;

namespace Chips.DependencyInjection
{
    public abstract class Bootstrapper<TContainer>
    {
        protected abstract TContainer CreateContainer();
        protected abstract void VerifyContainer();


        public static TContainer Container { get; protected set; }

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

        public static IEnumerable<Type> GetBootstrapTypes() =>
            from assembly in AppDomain.CurrentDomain.GetAssemblies()
            from type in assembly.GetExportedTypes()
            where typeof(IBootstrap<TContainer>).IsAssignableFrom(type) && !type.IsAbstract
            select type;
    }
}