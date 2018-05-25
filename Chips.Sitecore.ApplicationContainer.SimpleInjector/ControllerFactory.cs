using System;
using System.Web.Mvc;
using Chips.DependencyInjection;
using SimpleInjector;

namespace Chips.Sitecore.ApplicationContainer.SimpleInjector
{
    public class ControllerFactory: ControllerFactory<Container>
    {
        public ControllerFactory(IControllerFactory innerFactory) : base(innerFactory)
        {
        }

        protected override IController GetController(Type controllerType) => 
            Bootstrapper<Container>.Container.GetInstance(controllerType) as IController;
        
    }
}
