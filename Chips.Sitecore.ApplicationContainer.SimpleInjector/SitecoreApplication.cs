using Chips.DependencyInjection.SimpleInjector;
using Chips.Sitecore.ApplicationContainer.SimpleInjector;

[assembly: WebActivatorEx.PreApplicationStartMethod(
    typeof(SitecoreApplication), "PreStart")]
namespace Chips.Sitecore.ApplicationContainer.SimpleInjector
{
    
    public class SitecoreApplication
    {
        public static void PreStart()
        {
            var bootstrapper = new SimpleInjectorBootstrapper();
            bootstrapper.Bootstrap();
        }
    }
}
