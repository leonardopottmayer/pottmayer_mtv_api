using Autofac;
using Tars.Adapter.Cache.Memory.DI;

namespace Pottmayer.MTV.Adapter.Cache.DI
{
    public static class CacheAdapterDI
    {
        public static ContainerBuilder ConfigureMTVCacheAdapter(this ContainerBuilder builder)
        {
            builder.ConfigureMemoryCacheService();
            return builder;
        }
    }
}
