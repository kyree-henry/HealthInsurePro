using HealthInsurePro.Contract.AccountContracts;
using HealthInsurePro.Contract.UserContracts;

namespace HealthInsurePro.Application.Abstracts.Repositories
{
    public interface IAccountRepository
    {
        Task<TokenResponseModel> LoginAsync(TokenRequestModel model);
        Task<UserModel> RegisterAsync(RegisterModel data);
        Task<bool> IsEmailInUse(string email);
    }
}