using HealthInsurePro.Domain.Identity;

namespace HealthInsurePro.Application.Abstracts.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(ApplicationUser user, IList<string>? roles = null);
    }
}