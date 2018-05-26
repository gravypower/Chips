using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using NUnit.Framework;

namespace Chips.Reflection.Tests
{
    [TestFixture]
    public class TypeBuilderHelperTests
    {
        public TypeBuilder TypeBuilder;

        public class SomeAttribute : Attribute
        {

        }
        public class SomeAttributeOther : Attribute
        {

        }

        public class TestBaseType
        {
            public TestBaseType()
            {
            }

            [Some]
            public TestBaseType(string one)
            {
            }

            [SomeAttributeOther]
            public TestBaseType(string one, string two)
            {
            }

            public TestBaseType(string one, string two, string three = "Hello World")
            {
            }
        }

        [Test]
        public void Test()
        {
            var assemblyName = new AssemblyName { Name = "Gravypower.Kernel.Reflection.Tests" };
            var thisDomain = Thread.GetDomain();
            var asmBuilder = thisDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            var modBuilder = asmBuilder.DefineDynamicModule(asmBuilder.GetName().Name, false);

            var typeBuilder = modBuilder.DefineType(
                   "Test",
                   GetTypeAttributes(),
                   typeof(TestBaseType));

            typeBuilder.CreatePassThroughConstructors(typeof(TestBaseType));

            var sut = typeBuilder.CreateType();

            var con = sut.GetConstructors();

            Assert.IsTrue(con.Length == 4);

            Assert.IsTrue(con[1].GetParameters().Any(x => x.ParameterType == typeof(string)));
            Assert.IsTrue(con[1].GetCustomAttributes(typeof(SomeAttribute), false).Any());

            Assert.IsTrue(con[2].GetParameters().Any(x => x.ParameterType == typeof(string)));
            Assert.IsTrue(con[2].GetCustomAttributes(typeof(SomeAttributeOther), false).Any());

            Assert.IsTrue(con[3].GetParameters().Any(x => x.ParameterType == typeof(string)));
        }

        private static TypeAttributes GetTypeAttributes()
        {
            return TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AutoClass | TypeAttributes.AnsiClass
                   | TypeAttributes.BeforeFieldInit | TypeAttributes.AutoLayout;
        }
    }
}
