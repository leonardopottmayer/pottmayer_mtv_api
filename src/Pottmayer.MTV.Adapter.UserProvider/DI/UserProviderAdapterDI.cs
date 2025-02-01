using Autofac;
using Tars.Adapter.UserProvider.DI;

namespace Pottmayer.MTV.Adapter.UserProvider.DI
{
    public static class UserProviderAdapterDI
    {
        public static ContainerBuilder ConfigureMTVUserProviderAdapter(this ContainerBuilder builder)
        {
            builder.ConfigureUserProvider();
            return builder;
        }
    }
}
