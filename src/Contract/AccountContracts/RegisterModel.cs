using HealthInsurePro.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HealthInsurePro.Contract.AccountContracts
{
    public record RegisterModel
    {
        [DisplayName("First Name")]
        public string FirstName { get; set; } = default!;

        public string SurName { get; set; } = default!;

        [DisplayName("Email Address")]
        [EmailAddress]
        public string Email { get; set; } = default!;

        public UserType UserType { get; set; } = UserType.User;

        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "The password must be at least {2} characters long.")]
        public string Password { get; set; } = default!;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = default!;
    }
}
