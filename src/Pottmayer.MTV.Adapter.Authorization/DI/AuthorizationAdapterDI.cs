using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tars.Adapter.Authorization.DI;

namespace Pottmayer.MTV.Adapter.Authorization.DI
{
    public static class AuthorizationAdapterDI
    {
        public static IHostApplicationBuilder ConfigureMTVAuthorizationAdapter(this IHostApplicationBuilder builder)
        {
            builder.ConfigureTarsAuthorizationAdapter();
            builder.Services.AddAuthorization();

            return builder;
        }
    }
}
