using HealthInsurePro.Contract.AccountContracts;
using HealthInsurePro.Contract.UserContracts;
using MediatR;

namespace HealthInsurePro.Application.Commands
{
    public record RegisterCommand(RegisterModel model) : IRequest<UserModel>;
}