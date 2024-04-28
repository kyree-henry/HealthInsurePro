using HealthInsurePro.Contract.AccountContracts;

namespace HealthInsurePro.Application.Commands
{
    public record LoginCommand(TokenRequestModel model) : IRequest<TokenResponseModel>;
}