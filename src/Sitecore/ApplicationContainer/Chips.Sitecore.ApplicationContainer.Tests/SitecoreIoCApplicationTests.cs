using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Chips.Reflection;
using Chips.Sitecore.ApplicationContainer.Exceptions;
using Chips.Tests.Common;
using FluentAssertions;
using MultipleApplication;
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
            var extraAssemblies = new List<Assembly>
            {
                typeof(Application1).Assembly
            };

            //Act
            Action multipleApplications = () => 
                Isolated.Execute(SitecoreApplication.PreApplicationStart, extraAssemblies);

            //Assert
            multipleApplications.Should().Throw<MultipleApplicationFound>();


        }
    }
}
