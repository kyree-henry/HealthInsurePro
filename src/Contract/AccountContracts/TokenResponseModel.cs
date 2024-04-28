namespace HealthInsurePro.Contract.AccountContracts
{
    public record TokenResponseModel
    {
        public string FullNames { get; set; } = default!;

        public bool IsLockedOut { get; set; }
        public string Token { get; set; } = default!;
        public string Message { get; set; } = default!;
        public bool Succeeded { get; set; } = false;
    }
}