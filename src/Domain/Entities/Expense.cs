namespace HealthInsurePro.Domain.Entities
{
    public class Expense
    {
        [Key, ForeignKey(nameof(Claim))]
        public Guid ClaimId { get; set; }

        public ExpenseType ExpenseType { get; set; }

        // Name of the procedure or medication
        public string ExpenseName { get; set; } = default!;
        public DateTime ExpenseDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ExpenseAmount { get; set; }

        public virtual Claim Claim { get; set; } = default!;
    }
}