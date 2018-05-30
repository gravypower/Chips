using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Chips.Reflection.Tests
{
    [TestFixture]
    public class FluentTypeBuilderTests
    {
        [Test]
        public void BuiltTypeImplementsITest()
        {
            //Assign
            var typeBuilder = new FluentTypeBuilder(AppDomain.CurrentDomain)
                .SetAssemblyName("Test")
                .Implements<ITest>();

            typeBuilder.SetTypeName("Test").CreateType();

            var assembly = typeBuilder.ModuleBuilder.Assembly;

            //Act
            var definedTypes = assembly.DefinedTypes.Single(x => x.FullName == "Test");

            //Assert
            definedTypes.ImplementedInterfaces.Should().Contain(typeof(ITest));
        }

        [Test]
        public void BuiltTypeImplementsITestHasMethods()
        {
            //Assign
            var typeBuilder = new FluentTypeBuilder(AppDomain.CurrentDomain)
                .SetAssemblyName("Test")
                .Implements<ITestMethods>()
                .ImplementInterface();

            typeBuilder
                .SetTypeName("Test").CreateType();

            var assembly = typeBuilder.ModuleBuilder.Assembly;

            //Act
            var definedTypes = assembly.DefinedTypes.Single(x => x.FullName == "Test");

            //Assert
            definedTypes.ImplementedInterfaces.Should().Contain(typeof(ITestMethods));
        }

        [Test]
        public void GivenEmittedClassITestMethodsImplementsCanCallMethodThatReturnsAString()
        {
            //Assign
            var typeBuilder = new FluentTypeBuilder(AppDomain.CurrentDomain)
                .SetAssemblyName("Test")
                .Implements<ITestMethods>()
                .ImplementInterface();

            typeBuilder.SetTypeName("Test").CreateType().Save();

            //Act
            var testMethods = typeBuilder.CreateInstance();

            //Act
            testMethods.GetType().GetMethod("SomeOtherMethod").ReturnType.Should().Be<string>();
        }
    }

    public interface ITest { }

    public interface ITestMethods
    {
        void SomeMethod();
        string SomeOtherMethod();
    }
}
