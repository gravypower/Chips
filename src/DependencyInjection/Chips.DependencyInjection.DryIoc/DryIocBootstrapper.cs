using System.Collections.Generic;
using System.Reflection;
using DryIoc;

namespace Chips.DependencyInjection.DryIoc
{
    public class DryIocBootstrapper : Bootstrapper<Container>
    {

        public DryIocBootstrapper(IEnumerable<Assembly> assemblies) : base(assemblies)
        {
        }

        protected override Container CreateContainer()
        {
            return new Container();
        }

        protected override void VerifyContainer()
        {

            Container.Validate();
        }
    }
}