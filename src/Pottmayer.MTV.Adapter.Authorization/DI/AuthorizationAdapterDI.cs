using Autofac;
using Tars.Adapter.Authorization.DI;

namespace Pottmayer.MTV.Adapter.Authorization.DI
{
    public static class AuthorizationAdapterDI
    {
        public static ContainerBuilder ConfigureMTVAuthorizationAdapter(this ContainerBuilder builder)
        {
            builder.ConfigurePasswordHasher()
                   .ConfigureAuthService()
                   .ConfigureAuthorizationMiddlewareResultHandler();

            return builder;
        }
    }
}
