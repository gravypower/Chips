using System.Web.Mvc;
using DryIoc;

namespace Chips.Sitecore.ApplicationContainer.Dryioc
{
    public class InitialiseControllerFactory: InitialiseControllerFactory<Container>
    {
        protected override ControllerFactory<Container> GetControllerFactory(IControllerFactory innerFactory)
        {
            return new ControllerFactory(innerFactory);
        }
    }
}
