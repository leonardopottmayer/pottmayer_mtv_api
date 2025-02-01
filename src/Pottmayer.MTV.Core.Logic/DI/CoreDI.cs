using Autofac;
using Tars.Core.DI;

namespace Pottmayer.MTV.Core.Logic.Extensions
{
    public static class CoreDI
    {
        public static ContainerBuilder ConfigureMTVCore(this ContainerBuilder builder)
        {
            builder.ConfigureDateProvider()
                   .ConfigureCommandHandlers(typeof(CoreDI).Assembly);

            return builder;
        }
    }
}
