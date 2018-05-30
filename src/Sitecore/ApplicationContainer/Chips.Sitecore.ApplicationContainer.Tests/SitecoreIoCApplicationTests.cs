using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Chips.Reflection;
using Chips.Sitecore.ApplicationContainer.Exceptions;
using Chips.Tests.Common;
using FluentAssertions;
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
                //Act
                SitecoreApplication.PreApplicationStart();

                //Assert
                ApplicationSpy.PreApplicationStartWasCalled.Should().BeTrue();
            });
        }

        [Test]
        public void WhenSitecoreIoCApplicationShutdownsIApplicationApplicationShutdownIsCalled()
        {
            Isolated.Execute(() =>
            {
                //Act
                SitecoreApplication.ApplicationShutdown();

                //Assert
                ApplicationSpy.ApplicationShutdownWasCalled.Should().BeTrue();
            });
        }

        [Test]
        public void Isolated_WhenMoreThanOneApplicationExitsMultipleApplicationFoundThrown()
        {
            //Assign
            var applicationName = MethodBase.GetCurrentMethod().Name;
            var typeBuilder = new FluentTypeBuilder(AppDomain.CurrentDomain)
                .SetAssemblyName(applicationName)
                .Implements<ISitecoreApplication>()
                .ImplementInterface()
                .SetTypeName(applicationName)
                .CreateType()
                .Save();

            var extraAssemblies = new List<Assembly>
            {
                typeBuilder.ModuleBuilder.Assembly,
                typeof(ISitecoreApplication).Assembly,
                GetType().Assembly
            };


            //Act
            Action multipleApplications = () =>
            {
                Isolated.Execute(SitecoreApplication.PreApplicationStart, extraAssemblies);
            };

            //Assert
            multipleApplications.Should().Throw<MultipleApplicationFound>();

            //Annihilate
#if !NCRUNCH
            File.Delete(applicationName + ".dll");
#endif
        }
    }
}
