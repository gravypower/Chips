using System.Web.Mvc;
using SimpleInjector;

namespace Chips.Sitecore.ApplicationContainer{
    public class InitialiseControllerFactory: InitialiseControllerFactory<Container>
    {
        protected override ControllerFactory<Container> GetControllerFactory(IControllerFactory innerFactory)
            => new ControllerFactory(innerFactory);
    }
}
