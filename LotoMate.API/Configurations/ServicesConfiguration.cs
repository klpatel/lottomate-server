using AutoMapper;
using LotoMate.Framework.Filters;
using LotoMate.Identity.API.Configurations;
using LotoMate.Client.Api.Configurations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using LotoMate.Lottery.Api.Configurations;
using LotoMate.Lottery.Reports.Configurations;

namespace LotoMate.API.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("LotoMate.Identity.API", new OpenApiInfo { Title = "LotoMate.Identity.API", Version = "v1" });
                c.SwaggerDoc("LotoMate.Client.API", new OpenApiInfo { Title = "LotoMate.Client.API", Version = "v1" });
                c.SwaggerDoc("LotoMate.Lottery.API", new OpenApiInfo { Title = "LotoMate.Lottery.API", Version = "v1" });
                c.SwaggerDoc("LotoMate.Lottery.Reports", new OpenApiInfo { Title = "LotoMate.Lottery.Reports", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                var identityApiAssemblyXmlFile = $"LotoMate.Identity.API.xml";
                var identityApiAssemblyXmlPath = Path.Combine(AppContext.BaseDirectory, identityApiAssemblyXmlFile);
                c.IncludeXmlComments(identityApiAssemblyXmlPath);

                var clientApiAssemblyXmlFile = $"LotoMate.Client.API.xml";
                var clientApiAssemblyXmlPath = Path.Combine(AppContext.BaseDirectory, clientApiAssemblyXmlFile);
                c.IncludeXmlComments(clientApiAssemblyXmlPath);

                var lotoApiAssemblyXmlFile = $"LotoMate.Lottery.API.xml";
                var lotoApiAssemblyXmlPath = Path.Combine(AppContext.BaseDirectory, lotoApiAssemblyXmlFile);
                c.IncludeXmlComments(lotoApiAssemblyXmlPath);

                var lotoReportAssemblyXmlFile = $"LotoMate.Lottery.Reports.xml";
                var lotoReportAssemblyXmlPath = Path.Combine(AppContext.BaseDirectory, lotoReportAssemblyXmlFile);
                c.IncludeXmlComments(lotoReportAssemblyXmlPath);

            });

            return services;
        }
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var identityApiAssembly = Assembly.Load("LotoMate.Identity.API");
            var clientApiAssembly = Assembly.Load("LotoMate.Client.API");
            var lottoApiAssembly = Assembly.Load("LotoMate.Lottery.API");
            var lottoReportsAssembly = Assembly.Load("LotoMate.Lottery.Reports");

            string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //Add automapper
            services.AddAutoMapper(identityApiAssembly, clientApiAssembly,lottoApiAssembly, lottoReportsAssembly);
            //load service from APIs
            services.AddIdentityServices(configuration);
            services.AddClientServices(configuration);
            services.AddLottoServices(configuration);
            services.AddReportServices(configuration);
            // Add mediatR
            services.AddMediatR(typeof(LotoMate.API.Configurations.ServicesConfiguration).GetTypeInfo().Assembly);

            return services;
        }
        public static IServiceCollection AddMiddleware(this IServiceCollection services)
        {
            var identityApiAssembly = Assembly.Load("LotoMate.Identity.API");
            var clientApiAssembly = Assembly.Load("LotoMate.Client.API");
            var lottoApiAssembly = Assembly.Load("LotoMate.Lottery.API");
            var lottoReportsAssembly = Assembly.Load("LotoMate.Lottery.Reports");

            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            services.AddTransient<HttpEncodeActionFilter>();

            services
                .AddMvc(options =>
                {
                    options.EnableEndpointRouting = false;
                    options.Filters.Add(new AuthorizeFilter(policy));
                    options.Filters.AddService<HttpEncodeActionFilter>();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                })
                .AddApplicationPart(identityApiAssembly)
                .AddApplicationPart(clientApiAssembly)
                .AddApplicationPart(lottoApiAssembly)
                .AddApplicationPart(lottoReportsAssembly);
            return services;
        }

    }
}
