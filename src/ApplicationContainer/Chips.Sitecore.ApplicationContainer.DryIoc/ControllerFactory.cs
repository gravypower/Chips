using System;
using System.Web.Mvc;
using Chips.DependencyInjection.DryIoc;
using DryIoc;

namespace Chips.Sitecore.ApplicationContainer.DryIoc
{
    public class ControllerFactory: ControllerFactory<Container>
    {
        public ControllerFactory(IControllerFactory innerFactory) : base(innerFactory)
        {
        }

        protected override IController GetController(Type controllerType) =>
            DryIocBootstrapper.Container.Resolve(controllerType) as IController;
    }
}