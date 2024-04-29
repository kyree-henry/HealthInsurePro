using HealthInsurePro.Domain;

namespace HealthInsurePro.Contract.ExpenseContracts
{
    public record ExpenseModel
    {
        public Guid ClaimId { get; set; }

        public ExpenseType ExpenseType { get; set; }

        public string ExpenseName { get; set; } = default!;
        public DateTime ExpenseDate { get; set; }
        public decimal ExpenseAmount { get; set; }
    }
}
