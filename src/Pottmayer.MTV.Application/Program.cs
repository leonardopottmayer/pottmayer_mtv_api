using Autofac;
using Autofac.Extensions.DependencyInjection;
using Pottmayer.MTV.Adapter.Authorization.DI;
using Pottmayer.MTV.Adapter.Cache.DI;
using Pottmayer.MTV.Adapter.Data.DI;
using Pottmayer.MTV.Adapter.Rest.DI;
using Pottmayer.MTV.Adapter.UserProvider.DI;
using Pottmayer.MTV.Core.Domain.Modules.Auth.Profiles;
using Pottmayer.MTV.Core.Logic.DI;
using System.Reflection;
using Tars.Adapter.Authorization.DI;
using Tars.Adapter.Rest.Middlewares;
using Tars.Contracts.Adapter.Rest.Response;
using Tars.Core.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(AuthProfile).GetTypeInfo().Assembly);

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
    config.AddDefaultPipelineBehaviors();
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();

builder.ConfigureMTVAuthorizationAdapter();
builder.ConfigureMTVCacheAdapter();
builder.ConfigureMTVDataAdapter();
builder.ConfigureMTVRestAdapter();
builder.ConfigureMTVUserProviderAdapter();
builder.ConfigureMTVCore();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>((builder) =>
{
    builder.RegisterMTVStandardRepositories();
    builder.RegisterMTVCommandHandlers();
});

var app = builder.Build();

if (builder.Environment.IsDevelopment())
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

app.UseDefaultAuthenticationAndAuthorizationMiddlewares();

app.UseCors(cors => cors.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseMiddleware<ResponseWrappingMiddleware<DefaultApiResponse<object>>>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapControllerRoute(name: "apiVersion", pattern: "api/v{version}/{controller=Home}/{action=Index}");
});

app.Run();