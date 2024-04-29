using HealthInsurePro.Contract.ExpenseContracts;

namespace HealthInsurePro.Contract.ClaimContracts
{
    public record ClaimModel : CreateClaimModel
    {
        public Guid ClaimId { get; set; }
        public decimal TotalAmount { get; set; }
        public List<ExpenseModel>? Expenses { get; set; }
    }
}