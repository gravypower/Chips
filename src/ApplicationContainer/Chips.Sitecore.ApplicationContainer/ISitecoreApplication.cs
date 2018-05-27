namespace Chips.Sitecore.ApplicationContainer
{
    public interface ISitecoreApplication
    {
        void PreApplicationStart();
        void ApplicationShutdown();
    }
}
