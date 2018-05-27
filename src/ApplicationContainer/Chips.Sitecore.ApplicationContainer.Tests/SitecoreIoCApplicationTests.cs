using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Chips.Reflection;
using Chips.Tests.Common;
using NUnit.Framework;

namespace Chips.Sitecore.ApplicationContainer.Tests
{
    [TestFixture]
    public class SitecoreIoCApplicationTests
    {
        [Test]
        public void WhenSitecoreIoCApplicationStartsIApplicationPreApplicationStartIsCalled()
        {
            Isolated.Execute(() =>
            {
                SitecoreApplication.PreApplicationStart();
                Assert.That(ApplicationSpy.PreApplicationStartWasCalled, Is.True);
            });
        }

        [Test]
        public void WhenSitecoreIoCApplicationShutdownsIApplicationApplicationShutdownIsCalled()
        {
            Isolated.Execute(() =>
            {
                SitecoreApplication.ApplicationShutdown();
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
                typeof(ISitecoreApplication).Assembly,
                GetType().Assembly
            };

            Assert.Throws<MultipleApplicationFound>(() =>
                Isolated.Execute(SitecoreApplication.PreApplicationStart, extraAssemblies));

#if !NCRUNCH
            File.Delete(applicationName + ".dll");
#endif
        }

        private static FluentTypeBuilder CreateNewApplication(string applicationName)
        {
            var typeBuilder = new FluentTypeBuilder(AppDomain.CurrentDomain)
                .SetAssemblyName(applicationName)
                .Implements<ISitecoreApplication>()
                .ImplementInterface()
                .SetTypeName(applicationName)
                .CreateType()
                .Save();
            return typeBuilder;
        }
    }
}
