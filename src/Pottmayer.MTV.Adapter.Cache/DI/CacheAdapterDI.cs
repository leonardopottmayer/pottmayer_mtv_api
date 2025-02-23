using Microsoft.Extensions.Hosting;
using Tars.Adapter.Cache.Memory.DI;

namespace Pottmayer.MTV.Adapter.Cache.DI
{
    public static class CacheAdapterDI
    {
        public static IHostApplicationBuilder ConfigureMTVCacheAdapter(this IHostApplicationBuilder builder)
        {
            builder.ConfigureTarsMemoryCacheAdapter();
            return builder;
        }
    }
}
