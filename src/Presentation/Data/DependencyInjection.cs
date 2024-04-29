using Asp.Versioning;
using HealthInsurePro.Application.Abstracts.Services;
using HealthInsurePro.Infrastructure;
using HealthInsurePro.Application;
using HealthInsurePro.Presentation.Data.Swagger;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace HealthInsurePro.Presentation.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure();
            services.AddSingleton<IDatabaseConfiguration, DatabaseConfiguration>();

            return services;
        }

        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.OperationFilter<SwaggerDefaultValues>();
                c.AddSecurityDefinition("Bearer", new()
                {
                    Description = @"Enter 'Bearer' [Space] and your token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new ()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            services.ConfigureOptions<ConfigureSwaggerOptions>();

            return services;
        }

        public static IServiceCollection AddAuthenticationAndAuthorizationServices(this IServiceCollection services, IConfiguration config)
        {
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
            services.AddAuthentication(authentication =>
            {
                authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.SaveToken = true;
                option.TokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]!)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                option.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            context.Response.Headers.Append("Token-Expired", "true");

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization();
            return services;
        }


        public static IServiceCollection AddMvcServices(this IServiceCollection services)
        {
            services.AddHealthChecks()
                    .AddCheck<HealthCheck>(nameof(Presentation))
                    .AddCheck<Domain.HealthCheck>(nameof(Domain))
                    .AddCheck<Contract.HealthCheck>(nameof(Contract))
                    .AddCheck<Application.HealthCheck>(nameof(Application))
                    .AddCheck<Infrastructure.HealthCheck>(nameof(Infrastructure));

            services.AddControllers().AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddEndpointsApiExplorer();

            return services;
        }

    }
}
