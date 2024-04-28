using HealthInsurePro.Domain.Identity;

namespace HealthInsurePro.Application.Abstracts.Services
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}
