using HealthInsurePro.Contract.ClaimContracts;
using HealthInsurePro.Domain;

namespace HealthInsurePro.Application.Abstracts.Repositories
{
    public interface IClaimRepository
    {
        Task<IEnumerable<ClaimModel>> GetAsync(string policyHolderNationalId);
        Task<ClaimModel> ProcessClaimAsync(Guid claimId, ClaimStatus action);

        Task<ClaimModel> GetByIdAsync(Guid id);
        Task<ClaimModel> CreateAsync(CreateClaimModel data);
    }
}