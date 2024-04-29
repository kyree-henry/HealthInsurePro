using HealthInsurePro.Contract.PolicyHolderContracts;

namespace HealthInsurePro.Application.Queries
{
    public record GetPolicyHolderByIdQuery(Guid policyHolderId) : IRequest<PolicyHolderModel>;
}