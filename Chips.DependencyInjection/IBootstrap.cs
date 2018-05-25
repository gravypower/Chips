namespace Chips.DependencyInjection
{
    public interface IBootstrap<in TContainer>
    {
        void Bootstrap(TContainer container);
    }
}
