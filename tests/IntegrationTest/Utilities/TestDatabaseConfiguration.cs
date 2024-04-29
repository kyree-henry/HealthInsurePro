using HealthInsurePro.Application.Abstracts.Services;
using Microsoft.Extensions.Configuration;

namespace HealthInsurePro.IntegrationTest.Utilities
{
    public class TestDatabaseConfiguration : IDatabaseConfiguration
    {
        private readonly IConfiguration _configuration;

        public TestDatabaseConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("SqlConnectionString")!;
        }
    }
}