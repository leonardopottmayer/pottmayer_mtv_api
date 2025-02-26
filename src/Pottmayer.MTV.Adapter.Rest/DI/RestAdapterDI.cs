﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using Tars.Adapter.Rest.DI;
using Tars.Contracts.Adapter.Rest.Response;

namespace Pottmayer.MTV.Adapter.Rest.DI
{
    public static class RestAdapterDI
    {
        public static IHostApplicationBuilder ConfigureMTVRestAdapter(this IHostApplicationBuilder builder)
        {
            builder.ConfigureTarsRestAdapter();

            builder.Services.ConfigureMTVRestAdapterControllers();
            builder.Services.AddResponseWrappingMiddleware<DefaultApiResponse<object>>();

            return builder;
        }

        public static IServiceCollection ConfigureMTVRestAdapterControllers(this IServiceCollection services)
        {
            services.AddControllers()
                    .AddApplicationPart(typeof(RestAdapterDI).Assembly)
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    });

            services.AddRouting(options => options.LowercaseUrls = false);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Pottmayer MTV API v1",
                    Version = "v1",
                    Description = "",
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Description = "API Key Authorization. Example: 'X-API-KEY: {your_api_key}'",
                    Name = "X-API-KEY",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }
    }
}
