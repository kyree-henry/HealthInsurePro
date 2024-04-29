using HealthInsurePro.Contract.PolicyHolderContracts;

namespace HealthInsurePro.Application.Abstracts.Repositories
{
    public interface IPolicyHolderRepository
    {
        Task<IEnumerable<PolicyHolderModel>> GetAsync();
        Task<PolicyHolderModel> GetByIdAsync(Guid policyHolderId);
    }
}
