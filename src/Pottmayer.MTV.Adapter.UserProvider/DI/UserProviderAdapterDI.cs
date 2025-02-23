using Microsoft.Extensions.Hosting;
using Tars.Adapter.UserProvider.DI;

namespace Pottmayer.MTV.Adapter.UserProvider.DI
{
    public static class UserProviderAdapterDI
    {
        public static IHostApplicationBuilder ConfigureMTVUserProviderAdapter(this IHostApplicationBuilder builder)
        {
            builder.ConfigureTarsUserProviderAdapter();
            return builder;
        }
    }
}
