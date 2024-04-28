namespace HealthInsurePro.Contract.ClaimContracts
{
    public record ClaimModel : CreateClaimModel
    {
        public Guid ClaimId { get; set; }
    }
}
