using HealthInsurePro.Contract.ClaimContracts;

namespace HealthInsurePro.Application.Abstracts.Repositories
{
    public interface IClaimRepository
    {
        Task<IEnumerable<ClaimModel>> GetAsync(string policyHolderNationalId);


        Task<ClaimModel> GetByIdAsync(Guid id);
        Task<ClaimModel> CreateAsync(CreateClaimModel data);
    }
}