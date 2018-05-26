using System;
using System.Collections.Generic;
using System.Reflection;

namespace Chips.Tests.Common
{
    public static class Isolated
    {
        public static void Execute(Action action, IEnumerable<Assembly> extraAssemblies = null)
        {
            var domain = default(AppDomain);

            try
            {
                domain = AppDomain.CreateDomain("New App Domain: " + Guid.NewGuid(),
                    null,
                    new AppDomainSetup
                    {
                        ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                        ConfigurationFile = "app.config"
                    });

                if (extraAssemblies != null)
                {
                    foreach (var extraAssembly in extraAssemblies)
                    {
                        domain.Load(extraAssembly.GetName());
                    }
                }

                var domainDelegate = (AppDomainDelegate)domain.CreateInstanceAndUnwrap(
                    typeof(AppDomainDelegate).Assembly.FullName,
                    typeof(AppDomainDelegate).FullName);


                domainDelegate.Execute(action);
            }
            catch (TypeInitializationException e)
            {
                throw e.InnerException;
            }
            finally
            {
                if (domain != null)
                    AppDomain.Unload(domain);
            }
        }

        private class AppDomainDelegate : MarshalByRefObject
        {
            public void Execute(Action action)
            {
                action();
            }
        }
    }
}
