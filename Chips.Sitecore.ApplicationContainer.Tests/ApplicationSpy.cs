
namespace Chips.Sitecore.ApplicationContainer.Tests
{
    public class ApplicationSpy : ISitecoreApplication
    {
        public static bool PreApplicationStartWasCalled = false;
        public static bool ApplicationShutdownWasCalled = false;

        public void PreApplicationStart()
        {
            PreApplicationStartWasCalled = true;
        }

        public void ApplicationShutdown()
        {
            ApplicationShutdownWasCalled = true;
        }
    }
}