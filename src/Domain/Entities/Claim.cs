namespace HealthInsurePro.Domain.Entities
{
    public class Claim
    {
        public Guid ClaimId { get; set; }

        public string NationalId { get; set; } = default!;
        public ClaimStatus ClaimStatus { get; set; }

        public virtual ICollection<Expense>? Expenses { get; set; }
    }
}