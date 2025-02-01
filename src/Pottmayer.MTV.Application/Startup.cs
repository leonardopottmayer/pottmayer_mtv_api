using Autofac;
using Pottmayer.MTV.Adapter.Authorization.DI;
using Pottmayer.MTV.Adapter.Cache.DI;
using Pottmayer.MTV.Adapter.Data.DI;
using Pottmayer.MTV.Adapter.Data.Impl;
using Pottmayer.MTV.Adapter.Rest.DI;
using Pottmayer.MTV.Adapter.UserProvider.DI;
using Pottmayer.MTV.Core.Domain.Modules.Auth.Profiles;
using Pottmayer.MTV.Core.Logic.Extensions;
using Tars.Adapter.Authorization.Extensions;
using Tars.Adapter.Rest.Middlewares;
using Tars.Contracts.Adapter.Rest.Response;
using Tars.Core.DI;
using System.Reflection;

namespace Pottmayer.MTV.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AuthProfile).GetTypeInfo().Assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddHttpContextAccessor();
            services.AddCors();

            services.ConfigureMTVRestAdapterControllers();

            services.AddEndpointsApiExplorer();

            services.AddAuthorization();
            services.ConfigureAuthentication(Configuration);

            services.ConfigureDbContext<AppDbContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Pottmayer MTV v1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.ConfigureDefaultAuthMiddlewares();

            app.UseCors(cors => cors.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseMiddleware<ResponseWrappingMiddleware<DefaultApiResponse<object>>>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(name: "apiVersion", pattern: "api/v{version}/{controller=Home}/{action=Index}");
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.ConfigureTarsApplicationService();

            builder.ConfigureMTVCore();
            builder.ConfigureMTVDataAdapter();
            builder.ConfigureMTVAuthorizationAdapter();
            builder.ConfigureMTVRestAdapter();
            builder.ConfigureMTVUserProviderAdapter();
            builder.ConfigureMTVCacheAdapter();
        }
    }
}
