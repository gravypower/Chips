using Chips.DependencyInjection.DryIoc;

namespace Chips.Sitecore.ApplicationContainer.Dryioc
{
    public class DryiocApplication : ISitecoreApplication
    {
        public void PreApplicationStart()
        {
            var bootstrapper = new DryIocBootstrapper(SitecoreApplication.ApplicationAssemblies);
            bootstrapper.Bootstrap();
        }

        public void ApplicationShutdown()
        {
            DryIocBootstrapper.Container.Dispose();
        }
    }
}
