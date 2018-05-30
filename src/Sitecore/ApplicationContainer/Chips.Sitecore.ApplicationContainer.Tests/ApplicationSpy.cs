namespace Chips.Sitecore.ApplicationContainer.Tests
{
    public class ApplicationSpy : ISitecoreApplication
    {
        public static bool PreApplicationStartWasCalled;
        public static bool ApplicationShutdownWasCalled;

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