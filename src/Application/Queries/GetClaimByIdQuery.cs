using HealthInsurePro.Contract.ClaimContracts;

namespace HealthInsurePro.Application.Queries
{
    public record GetClaimByIdQuery(Guid claimId) : IRequest<ClaimModel>;
}