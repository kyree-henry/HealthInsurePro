using System.ComponentModel.DataAnnotations;

namespace HealthInsurePro.Contract.AccountContracts
{
    public record TokenRequestModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}