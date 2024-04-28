using HealthInsurePro.Contract.ClaimContracts;

namespace HealthInsurePro.Application.Commands
{
    public record CreateClaimCommand(CreateClaimModel model) : IRequest<ClaimModel>;
}