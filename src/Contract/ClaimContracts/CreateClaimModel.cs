using HealthInsurePro.Domain;

namespace HealthInsurePro.Contract.ClaimContracts
{
    public record CreateClaimModel
    {
        public string NationalId { get; set; } = default!;
        public ClaimStatus ClaimStatus { get; set; }
    }
}