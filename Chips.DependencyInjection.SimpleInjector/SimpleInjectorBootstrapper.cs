using SimpleInjector;

namespace Chips.DependencyInjection.SimpleInjector
{
    public class SimpleInjectorBootstrapper : Bootstrapper<Container>
    {
        protected override Container CreateContainer()
        {
            return new Container();
        }

        protected override void VerifyContainer()
        {
#if DEBUG
            Container.Verify(VerificationOption.VerifyAndDiagnose);
#else
            Container.Verify();
#endif
        }
    }
}