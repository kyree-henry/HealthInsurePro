namespace HealthInsurePro.Contract.UserContracts
{
    public record UserModel
    {
        public string FullNames { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
