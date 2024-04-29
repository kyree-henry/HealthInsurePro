using HealthInsurePro.Application.Abstracts.Services;
using HealthInsurePro.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace HealthInsurePro.Infrastructure.Services
{
    internal class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly DataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public DatabaseSeeder(DataContext context, 
                              UserManager<ApplicationUser> userManager,
                              RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Initialize()
        {

        }


    }
}
