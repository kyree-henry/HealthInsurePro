using HealthInsurePro.Infrastructure.Extensions;

namespace HealthInsurePro.Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            await host.ConfigureHost().RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog((ctx, lc) => lc.WriteTo.Console()
            .ReadFrom.Configuration(ctx.Configuration))
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStaticWebAssets();
                webBuilder.UseStartup<Startup>();
            });
    }
}