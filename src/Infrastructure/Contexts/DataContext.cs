using HealthInsurePro.Domain.Entities;
using HealthInsurePro.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthInsurePro.Infrastructure.Contexts
{
    public class DataContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Claim> Claims => Set<Claim>();
        public DbSet<PolicyHolder> PolicyHolders => Set<PolicyHolder>();
    }
}