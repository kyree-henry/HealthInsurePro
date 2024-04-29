using HealthInsurePro.Contract.PolicyHolderContracts;

namespace HealthInsurePro.Application.Queries
{
    public record GetPolicyHoldersQuery() : IRequest<IEnumerable<PolicyHolderModel>>;
}