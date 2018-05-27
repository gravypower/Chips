using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Chips.Reflection
{
    public class FluentTypeBuilder
    {
        /// <summary>
        /// The interface type attributes.
        /// </summary>
        public const TypeAttributes InterfaceTypeAttributes =
            TypeAttributes.Public | TypeAttributes.Interface | TypeAttributes.Abstract;

        /// <summary>
        /// The class type attributes.
        /// </summary>
        public const TypeAttributes ClassTypeAttributes =
            TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AutoClass |
            TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit | TypeAttributes.AutoLayout;

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentTypeBuilder"/> class.
        /// </summary>
        /// <param name="appDomain">
        /// The app domain.
        /// </param>
        public FluentTypeBuilder(_AppDomain appDomain)
        {
            AppDomain = appDomain;

            Interfaces = new List<Type>();
            BaseType = typeof (object);
            TypeName = "FluentTypeBuilder";
            AssemblyName = new AssemblyName {Name = "FluentTypeBuilder"};
            MakeBuilders();
        }

        /// <summary>
        /// Gets the base type.
        /// </summary>
        /// <value>
        /// The base type.
        /// </value>
        public Type BaseType { get; private set; }

        /// <summary>
        /// Gets the type that is being build.
        /// </summary>
        /// <value>
        /// The type that has been build.
        /// </value>
        public Type Type { get; private set; }

        /// <summary>
        /// Gets the type name.
        /// </summary>
        /// <value>
        /// The type name.
        /// </value>
        public string TypeName { get; private set; }

        /// <summary>
        /// Gets the interfaces.
        /// </summary>
        /// <value>
        /// The interfaces.
        /// </value>
        public List<Type> Interfaces { get; private set; }

        /// <summary>
        /// Gets the assembly name.
        /// </summary>
        /// <value>
        /// The assembly name.
        /// </value>
        public AssemblyName AssemblyName { get; private set; }

        /// <summary>
        /// Gets the module builder.
        /// </summary>
        /// <value>
        /// The module builder.
        /// </value>
        public ModuleBuilder ModuleBuilder { get; private set; }

        /// <summary>
        /// Gets the type builder.
        /// </summary>
        /// <value>
        /// The type builder.
        /// </value>
        public TypeBuilder TypeBuilder { get; private set; }

        /// <summary>
        /// Gets the assembly builder.
        /// </summary>
        /// <value>
        /// The assembly builder.
        /// </value>
        public AssemblyBuilder AssemblyBuilder { get; private set; }

        private bool implementInterface { get; set; }

        private _AppDomain AppDomain { get; set; }

        /// <summary>
        /// The create type.
        /// </summary>
        /// <returns>
        /// The <see cref="FluentTypeBuilder"/>.
        /// </returns>
        public FluentTypeBuilder CreateType()
        {
            TypeBuilder = ModuleBuilder.DefineType(
                TypeName,
                ClassTypeAttributes,
                BaseType,
                Interfaces.ToArray());

            if (implementInterface)
                ImplementInterfaceHelper();

            TypeBuilder.CreatePassThroughConstructors(BaseType);
            Type = TypeBuilder.CreateType();

            return this;
        }

        public FluentTypeBuilder Save()
        {
            AssemblyBuilder.Save(AssemblyName.FullName + ".dll");
            return this;
        }

        /// <summary>
        /// The create instance.
        /// </summary>
        /// <param name="constructorArguments">
        /// The arguments for the constructor.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object CreateInstance(object[] constructorArguments = null)
        {
            if (Type == null)
            {
                return CreateType().CreateInstance();
            }

            if (Type.IsInterface)
            {
                return new FluentTypeBuilder(AppDomain).Implements(Type).CreateInstance();
            }

            return Activator.CreateInstance(Type, constructorArguments);
        }

        /// <summary>
        /// The implements.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the interface.
        /// </typeparam>
        /// <returns>
        /// The <see cref="FluentTypeBuilder"/>.
        /// </returns>
        public FluentTypeBuilder Implements<T>()
        {
            return Implements(typeof (T));
        }

        /// <summary>
        /// The implements.
        /// </summary>
        /// <param name="type">
        /// The type of the interface.
        /// </param>
        /// <returns>
        /// The <see cref="FluentTypeBuilder"/>.
        /// </returns>
        public FluentTypeBuilder Implements(Type type)
        {
            Interfaces.Add(type);
            return this;
        }

        /// <summary>
        /// The base type of.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the interface.
        /// </typeparam>
        /// <returns>
        /// The <see cref="FluentTypeBuilder"/>.
        /// </returns>
        public FluentTypeBuilder BaseTypeOf<T>()
        {
            return BaseTypeOf(typeof (T));
        }

        /// <summary>
        /// The base type of.
        /// </summary>
        /// <param name="type">
        /// The type that the new type will be based off.
        /// </param>
        /// <returns>
        /// The <see cref="FluentTypeBuilder"/>.
        /// </returns>
        public FluentTypeBuilder BaseTypeOf(Type type)
        {
            BaseType = type;
            return this;
        }


        /// <summary>
        /// The set assembly name.
        /// </summary>
        /// <param name="assemblyName">
        /// The assembly name.
        /// </param>
        /// <returns>
        /// The <see cref="FluentTypeBuilder"/>.
        /// </returns>
        public FluentTypeBuilder SetAssemblyName(string assemblyName)
        {
            AssemblyName.Name = assemblyName;
            MakeBuilders();
            return this;
        }

        /// <summary>
        /// The set type name.
        /// </summary>
        /// <param name="name">
        /// The name of the type we are building.
        /// </param>
        /// <returns>
        /// The <see cref="FluentTypeBuilder"/>.
        /// </returns>
        public FluentTypeBuilder SetTypeName(string name)
        {
            TypeName = name;
            return this;
        }

        /// <summary>
        /// The create interface.
        /// </summary>
        /// <returns>
        /// The <see cref="FluentTypeBuilder"/>.
        /// </returns>
        public FluentTypeBuilder CreateInterface()
        {
            Type =
                ModuleBuilder.DefineType(TypeName, InterfaceTypeAttributes, null, Interfaces.ToArray())
                    .CreateType();

            return this;
        }

        private void MakeBuilders()
        {
            AssemblyBuilder = AppDomain.DefineDynamicAssembly(AssemblyName, AssemblyBuilderAccess.RunAndSave);
            var assemblyName = AssemblyBuilder.GetName().Name;
            ModuleBuilder = AssemblyBuilder.DefineDynamicModule(assemblyName, assemblyName +".dll");
        }

        public FluentTypeBuilder ImplementInterface()
        {
            implementInterface = true;
            return this;
        }

        private void ImplementInterfaceHelper()
        {
            foreach (var method in Interfaces.SelectMany(i => i.GetMethods()))
            {
                var newMethod = TypeBuilder.DefineMethod(
                    method.Name,
                    method.Attributes ^ MethodAttributes.Abstract,
                    method.CallingConvention,
                    method.ReturnType,
                    method.ReturnParameter.GetRequiredCustomModifiers(),
                    method.ReturnParameter.GetOptionalCustomModifiers(),
                    method.GetParameters().Select(p => p.ParameterType).ToArray(),
                    method.GetParameters().Select(p => p.GetRequiredCustomModifiers()).ToArray(),
                    method.GetParameters().Select(p => p.GetOptionalCustomModifiers()).ToArray()
                    );

                var methodBody = newMethod.GetILGenerator();

                var hasReturnType = method.ReturnType != typeof(void);
               
                if (hasReturnType)
                {
                    methodBody.Emit(OpCodes.Ldnull);
                }

                // return;
                methodBody.Emit(OpCodes.Ret);
            }
        }

    }


}
