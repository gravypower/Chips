using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Chips.Sitecore.ApplicationContainer;
using Chips.Sitecore.ApplicationContainer.Exceptions;
using Chips.Sitecore.ApplicationContainer.Properties;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SitecoreApplication), "PreApplicationStart")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(SitecoreApplication), "ApplicationShutdown")]
namespace Chips.Sitecore.ApplicationContainer
{
    public class SitecoreApplication
    {
        public static readonly IEnumerable<Assembly> ApplicationAssemblies;
        private static readonly ISitecoreApplication Application;

        static SitecoreApplication()
        {
            ApplicationAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic)
                .Where(a => !Settings.Default.IgnoreProductList.Contains(a.GetAssemblyAttribute<AssemblyProductAttribute>(ass => ass.Product)))
                .Where(a => !Settings.Default.IgnoreCompaniesList.Contains(a.GetAssemblyAttribute<AssemblyCompanyAttribute>(ass => ass.Company)));

            var applications = ApplicationAssemblies.SelectMany(a => a.GetTypes())
                .Where(t => !t.IsInterface)
                .Where(t => typeof(ISitecoreApplication).IsAssignableFrom(t)).ToList();

            try
            {
                var applicationType = applications.Single();
                Application = (ISitecoreApplication)Activator.CreateInstance(applicationType);
            }
            catch (InvalidOperationException e)
            {
                switch (e.Message)
                {
                    case "Sequence contains more than one element":
                        throw new MultipleApplicationFound(applications);
                    case "Sequence contains no elements":
                        throw new NoApplicationFound();
                    default:
                        throw;
                }
            }
            catch (ReflectionTypeLoadException re)
            {
                var message = "Could not load types: \n";
                foreach (var loaderException in re.LoaderExceptions)
                {
                    message += loaderException.Message + "\n";
                }

                throw new Exception(message);
            }
        }

        public static void PreApplicationStart()
        {
            global::Sitecore.Diagnostics.Log.Info($"Starting {Application.GetType().FullName}", Application);
            Application.PreApplicationStart();
        }

        public static void ApplicationShutdown()
        {
            Application.ApplicationShutdown();
        }
    }
}
