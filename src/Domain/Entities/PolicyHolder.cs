namespace HealthInsurePro.Domain.Entities
{
    public class PolicyHolder
    {
        public Guid PolicyHolderId { get; set; }

        public string FirstName { get; set; } = default!;
        public string SurName { get; set; } = default!;

        public string NationalId { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public string PolicyNumber { get; set; } = default!;
    }
}