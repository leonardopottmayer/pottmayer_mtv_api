using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pottmayer.MTV.Adapter.Data.Impl;
using Tars.Adapter.Data.DI;
using Tars.Contracts.Adapter.Data;

namespace Pottmayer.MTV.Adapter.Data.DI
{
    public static class DataAdapterDI
    {
        public static IHostApplicationBuilder ConfigureMTVDataAdapter(this IHostApplicationBuilder builder)
        {
            AddDapperTypeHandlers();

            builder.ConfigureTarsDataAdapter();
            builder.Services.ConfigureDbContext<AppDbContext>();

            return builder;
        }

        public static void AddDapperTypeHandlers()
        {
            Tars.Adapter.Data.DI.DataAdapterDI.AddDapperBooleanStringTypeHandler();
        }

        public static ContainerBuilder RegisterMTVStandardRepositories(this ContainerBuilder builder)
        {
            builder.RegisterStandardRepositories(typeof(DataAdapterDI).Assembly);
            return builder;
        }

        public static IServiceCollection ConfigureDbContext<TDbContext>(this IServiceCollection serviceCollection) where TDbContext : DbContext
        {
            serviceCollection.AddDbContext<TDbContext>((sp, options) =>
            {
                var dataConfigResolver = sp.GetRequiredService<IDataConfigResolver>();
                DataConfig dataConfig = dataConfigResolver.Resolve();

                options.UseNpgsql(dataConfig.ConnectionString);
            });

            return serviceCollection;
        }
    }
}
