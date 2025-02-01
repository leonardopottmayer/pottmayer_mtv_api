using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tars.Adapter.Data.DI;
using Tars.Contracts.Adapter.Data;
using Tars.Contracts.Application.Config;

namespace Pottmayer.MTV.Adapter.Data.DI
{
    public static class DataAdapterDI
    {
        public static ContainerBuilder ConfigureMTVDataAdapter(this ContainerBuilder builder)
        {
            builder.RegisterRepositoryResolver()
                   .RegisterConnectionStringResolver()
                   .RegisterDbConnectionFactory()
                   .RegisterDataContextFactory()
                   .RegisterStandardRepositories(typeof(DataAdapterDI).Assembly);

            Tars.Adapter.Data.DI.DataAdapterDI.ConfigureDapperTypeHandlers();

            return builder;
        }

        public static IServiceCollection ConfigureDbContext<TDbContext>(this IServiceCollection serviceCollection) where TDbContext : DbContext
        {
            serviceCollection.AddDbContext<TDbContext>((serviceProvider, options) =>
            {
                var dataConfigResolver = serviceProvider.GetRequiredService<IDataConfigResolver>();
                DataConfig dataConfig = dataConfigResolver.Resolve();

                options.UseNpgsql(dataConfig.ConnectionString);
            });

            return serviceCollection;
        }
    }
}
