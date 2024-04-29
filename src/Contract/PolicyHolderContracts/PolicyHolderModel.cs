namespace HealthInsurePro.Contract.PolicyHolderContracts
{
    public record PolicyHolderModel : CreatePolicyHolderModel
    {
        public int Age { get; set; }
        public string FullNames { get; set; } = default!;
    }
}