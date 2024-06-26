﻿using HealthInsurePro.Application.Abstracts.Services;

namespace HealthInsurePro.Presentation.Data
{
    public class DatabaseConfiguration : IDatabaseConfiguration
    {
        private readonly IConfiguration _configuration;

        public DatabaseConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
            return _configuration!.GetConnectionString("SqlConnectionString")!;
        }
    }
}