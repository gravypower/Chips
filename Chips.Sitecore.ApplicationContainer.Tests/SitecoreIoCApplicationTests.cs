using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Chips.Sitecore.ApplicationContainer.Tests;
using Gravypower.Kernel.Reflection;
using NUnit.Framework;
using SitecoreIoC.Tests.Common;

namespace SitecoreIoC.Tests
{
    [TestFixture]
    public class SitecoreIoCApplicationTests
    {
        [Test]
        public void WhenSitecoreIoCApplicationStartsIApplicationPreApplicationStartIsCalled()
        {
            Isolated.Execute(() =>
            {
                SitecoreIoCApplication.PreApplicationStart();
                Assert.That(ApplicationSpy.PreApplicationStartWasCalled, Is.True);
            });
        }

        [Test]
        public void WhenSitecoreIoCApplicationShutdownsIApplicationApplicationShutdownIsCalled()
        {
            Isolated.Execute(() =>
            {
                SitecoreIoCApplication.ApplicationShutdown();
                Assert.That(ApplicationSpy.ApplicationShutdownWasCalled, Is.True);
            });
        }

        [Test]
        public void Isolated_WhenMoreThanOneApplicationExitsMultipleApplicationFoundThrown()
        {
            var applicationName = MethodBase.GetCurrentMethod().Name;
            var typeBuilder = CreateNewApplication(applicationName);

            var extraAssemblies = new List<Assembly>
            {
                typeBuilder.ModuleBuilder.Assembly,
                typeof(IApplication).Assembly,
                GetType().Assembly
            };

            Assert.Throws<MultipleApplicationFound>(() =>
                Isolated.Execute(SitecoreIoCApplication.PreApplicationStart, extraAssemblies));

#if !NCRUNCH
            File.Delete(applicationName + ".dll");
#endif
        }

        [Test]
        public void WhenSitecoreIoCApplicationStartsIApplicationCreateNewApplicationIsCalled()
        {
            Isolated.Execute(() =>
            {
                SitecoreIoCApplication.GetControllerFactory();
                Assert.That(ApplicationSpy.GetControllerFactoryWasCalled, Is.True);
            });
        }

        [Test]
        public void WhenProcessIsCalledOnInitialiseControllerFactoryGetControllerFactoryWasCalled()
        {
            Isolated.Execute(() =>
            {
                var initialiseControllerFactory = new InitialiseControllerFactory();
                initialiseControllerFactory.Process(null);
                Assert.That(ApplicationSpy.GetControllerFactoryWasCalled, Is.True);
            });
        }

        private static FluentTypeBuilder CreateNewApplication(string applicationName)
        {
            var typeBuilder = new FluentTypeBuilder(AppDomain.CurrentDomain)
                .SetAssemblyName(applicationName)
                .Implements<IApplication>()
                .ImplementInterface()
                .SetTypeName(applicationName)
                .CreateType()
                .Save();
            return typeBuilder;
        }
    }
}
