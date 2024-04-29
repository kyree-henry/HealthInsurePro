using HealthInsurePro.Application.Abstracts.Services;
using HealthInsurePro.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HealthInsurePro.Infrastructure.Contexts
{
    public class DataContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private readonly IDatabaseConfiguration _config;

        public DataContext(DbContextOptions<DataContext> options, IDatabaseConfiguration config)
            : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString(), b => b.MigrationsAssembly("HealthInsurePro.Presentation"));
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Claim> Claims => Set<Claim>();
        public DbSet<PolicyHolder> PolicyHolders => Set<PolicyHolder>();
    }
}