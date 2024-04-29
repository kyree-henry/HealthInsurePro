using HealthInsurePro.Application;
using HealthInsurePro.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HealthInsurePro.IntegrationTest.Utilities
{
    public static class TestHost
    {
        private static bool _dependenciesRegistered;
        private static readonly object Lock = new();
        private static IHost? _host;

        public static IHost Instance
        {
            get
            {
                EnsureDependenciesRegistered();
                return _host!;
            }
        }

        public static T GetRequiredService<T>() where T : notnull
        {
            IServiceScope serviceScope = Instance.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;
            return provider.GetRequiredService<T>();
        }

        private static void Initialize()
        {
            IHost host = Host.CreateDefaultBuilder()
                .UseEnvironment("Development")
                .ConfigureAppConfiguration((context, config) =>
                {
                    IHostEnvironment env = context.HostingEnvironment;

                    config
                        .AddJsonFile("appsettings.json", false, true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                        .AddEnvironmentVariables();
                })
                .ConfigureServices(s =>
                {
                    s.AddApplication();
                    s.AddInfrastructure();
                })
                .Build();


            _host = host;
        }

        private static void EnsureDependenciesRegistered()
        {
            if (!_dependenciesRegistered)
                lock (Lock)
                {
                    if (!_dependenciesRegistered)
                    {
                        Initialize();
                        _dependenciesRegistered = true;
                    }
                }
        }
    }
}
