using System.Web.Mvc;
using SimpleInjector;
using SimpleInjector.Diagnostics;

namespace Chips.DependencyInjection.SimpleInjector.Extensions
{
    public static class ContainerExtensions
    {
        private static void RegisterController<TController>(this Container container)
            where TController : IController
        {
            var controllerType = typeof(TController);
            var lifestyle = container.Options.LifestyleSelectionBehavior.SelectLifestyle(controllerType);
            var registration = lifestyle.CreateRegistration(controllerType, container);

            // Microsoft.AspNetCore.Mvc.Controller implements IDisposable (which is a design flaw).
            // This will cause false positives in Simple Injector's diagnostic services, so we suppress
            // this warning in case the registered type doesn't override Dispose from Controller.
            registration.SuppressDiagnosticWarning(
                DiagnosticType.DisposableTransientComponent,
                "Derived type doesn't override Dispose, so it can be safely ignored.");

            container.AddRegistration<IController>(registration);
        }
    }
}
