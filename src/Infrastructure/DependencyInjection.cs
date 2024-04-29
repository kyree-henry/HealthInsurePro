using HealthInsurePro.Application.Abstracts.Services;
using HealthInsurePro.Domain.Identity;
using HealthInsurePro.Infrastructure.Repositories;
using HealthInsurePro.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HealthInsurePro.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<DbContext, DataContext>();
            services.AddDbContextFactory<DataContext>();
            services.AddDbContextFactory<DbContext>();

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.SignIn.RequireConfirmedEmail = false;
                options.User.AllowedUserNameCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@.-/_";
            }).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();

            services.AddScoped<IClaimRepository, ClaimRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IPolicyHolderRepository, PolicyHolderRepository>();

            return services;
        }
    }
}
