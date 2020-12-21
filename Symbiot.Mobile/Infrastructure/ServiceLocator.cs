using DryIoc;

namespace Symbiot.Mobile.Infrastructure
{
    public static class ServiceLocator
    {
        private static IContainer container;

        public static void Initialize(IContainer dryContainer)
        {
            ServiceLocator.container = dryContainer;
        }

        public static TType GetInstance<TType>()
        {
            return container.Resolve<TType>();
        }
    }
}