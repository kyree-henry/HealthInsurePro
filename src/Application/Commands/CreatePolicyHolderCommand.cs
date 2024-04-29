using HealthInsurePro.Contract.PolicyHolderContracts;

namespace HealthInsurePro.Application.Commands
{
    public record CreatePolicyHolderCommand(CreatePolicyHolderModel model) : IRequest<PolicyHolderModel>;
}