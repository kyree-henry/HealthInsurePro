using HealthInsurePro.Contract.ClaimContracts;

namespace HealthInsurePro.Application.Queries
{
    public record GetClaimsQuery(string policyHolderNationalId) : IRequest<IEnumerable<ClaimModel>>;
}