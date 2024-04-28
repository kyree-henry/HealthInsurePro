namespace HealthInsurePro.Domain.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = default!;
        public string SurName { get; set; } = default!;

        public UserType UserType { get; set; }
    }
}
