using Autofac;
using Microsoft.Extensions.Hosting;
using Tars.Core.DI;

namespace Pottmayer.MTV.Core.Logic.DI
{
    public static class CoreDI
    {
        public static IHostApplicationBuilder ConfigureMTVCore(this IHostApplicationBuilder builder)
        {
            builder.ConfigureTarsCore();
            return builder;
        }

        public static ContainerBuilder RegisterMTVCommandHandlers(this ContainerBuilder builder)
        {
            builder.RegisterCommandHandlers(typeof(CoreDI).Assembly);
            return builder;
        }
    }
}
