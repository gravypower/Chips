using System;
using System.Linq;
using NUnit.Framework;

namespace Chips.Reflection.Tests
{
    [TestFixture]
    public class FluentTypeBuilderTests
    {
        [Test]
        public void BuiltTypeImplementsITest()
        {
            var typeBuilder = new FluentTypeBuilder(AppDomain.CurrentDomain)
                .SetAssemblyName("Test")
                .Implements<ITest>();

            typeBuilder
                .SetTypeName("Test").CreateType();

            var assembly = typeBuilder.ModuleBuilder.Assembly;
            var definedTypes = assembly.DefinedTypes.Single(x => x.FullName == "Test");
            Assert.That(definedTypes.ImplementedInterfaces, Contains.Item(typeof(ITest)));
        }

        [Test]
        public void BuiltTypeImplementsITestHasMethods()
        {
            var typeBuilder = new FluentTypeBuilder(AppDomain.CurrentDomain)
                .SetAssemblyName("Test")
                .Implements<ITestMethods>()
                .ImplementInterface();

            typeBuilder
                .SetTypeName("Test").CreateType();

            var assembly = typeBuilder.ModuleBuilder.Assembly;
            var definedTypes = assembly.DefinedTypes.Single(x => x.FullName == "Test");
            Assert.That(definedTypes.ImplementedInterfaces, Contains.Item(typeof(ITestMethods)));
        }

        [Test]
        public void GivenEmittedClassITestMethodsImplementsCanCallMethodThatReturnsAString()
        {
            var typeBuilder = new FluentTypeBuilder(AppDomain.CurrentDomain)
                .SetAssemblyName("Test")
                .Implements<ITestMethods>()
                .ImplementInterface();

            typeBuilder
                .SetTypeName("Test").CreateType().Save();

            var assembly = typeBuilder.ModuleBuilder.Assembly;

            var testMethods = (ITestMethods)assembly.CreateInstance("Test");

            testMethods.SomeOtherMethod();
        }

    }

    public interface ITest { }

    public interface ITestMethods
    {
        void SomeMethod();
        string SomeOtherMethod();
    }
}
