using HealthInsurePro.Application.Abstracts.Services;
using HealthInsurePro.Domain;
using HealthInsurePro.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Serilog;

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
            await AddAdministrator();
        }

        private async Task AddAdministrator()
        {
            //Check if Role Exists
            ApplicationRole adminRole = new(RoleConstants.AdminRole, "Administrator role with full permissions");
            ApplicationRole? adminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.AdminRole);
            if (adminRoleInDb is null)
            {
                await _roleManager.CreateAsync(adminRole);
                adminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.AdminRole);
                Log.Information("Seeded Administrator Role.");
            }

            ApplicationUser adminuser = new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Admin",
                SurName = "User",
                UserType = UserType.Admin,
                Email = "admin@healthinsurepro.com",
                UserName = "admin@healthinsurepro.com",
                EmailConfirmed = true,
            };

            ApplicationUser? superUserInDb = await _userManager.FindByEmailAsync(adminuser.Email);
            if (superUserInDb is null)
            {
                IdentityResult? result = await _userManager.CreateAsync(adminuser, RoleConstants.DefaultPassword);
                if (result.Succeeded)
                {
                    result = await _userManager.AddToRoleAsync(adminuser, RoleConstants.AdminRole);
                    if (result.Succeeded)
                    {
                        Log.Information("Seeded Default Admin User.");
                    }
                    else
                    {
                        LogIdentityErrors(result.Errors);
                    }
                }
                else
                {
                    LogIdentityErrors(result.Errors);
                }

            }

        }

        private static void LogIdentityErrors(IEnumerable<IdentityError> errors)
        {
            foreach (IdentityError? error in errors)
            {
                Log.Error(error.Description);
            }
        }
    }
}
