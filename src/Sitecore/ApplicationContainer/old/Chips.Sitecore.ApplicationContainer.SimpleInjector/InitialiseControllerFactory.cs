using System.Web.Mvc;
using SimpleInjector;

namespace Chips.Sitecore.ApplicationContainer.SimpleInjector
{
    public class InitialiseControllerFactory: InitialiseControllerFactory<Container>
    {
        protected override ControllerFactory<Container> GetControllerFactory(IControllerFactory innerFactory)
            => new ControllerFactory(innerFactory);
    }
}
