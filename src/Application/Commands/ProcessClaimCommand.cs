using HealthInsurePro.Contract.ClaimContracts;
using HealthInsurePro.Domain;

namespace HealthInsurePro.Application.Commands
{
    public record ProcessClaimCommand(Guid claimId, ClaimStatus action) : IRequest<ClaimModel>;
}