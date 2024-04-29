namespace HealthInsurePro.Contract.PolicyHolderContracts
{
    public record CreatePolicyHolderModel
    {
        public Guid PolicyHolderId { get; set; }

        public string PolicyNumber { get; set; } = default!;
        public string NationalId { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
    }
}