using HealthInsurePro.Application.Abstracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HealthInsurePro.Infrastructure.Extensions
{
    public static class HostExtensions
    {
        public static IHost ConfigureHost(this IHost host)
        {
            BootstrapDatabaseAsync(host);
            return host;
        }

        private static async void BootstrapDatabaseAsync(IHost host)
        {
            using IServiceScope scope = ServiceProviderServiceExtensions.CreateScope(host.Services);

            // Run Health Check Services
            await ServiceProviderServiceExtensions.GetRequiredService<HealthCheckService>(scope.ServiceProvider).CheckHealthAsync();

            // Migrate Database
            DataContext? context = ServiceProviderServiceExtensions.GetRequiredService<DataContext>(scope.ServiceProvider);
            await context?.Database?.MigrateAsync()!;

            // Initialize Database Seed
            IDatabaseSeeder service = ServiceProviderServiceExtensions.GetService<IDatabaseSeeder>(scope.ServiceProvider)!;
            if (service != null)
            {
                await service.Initialize();
            }
        }
    }
}