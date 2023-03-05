using System.ComponentModel.DataAnnotations;

namespace emarketo.Models.Forms
{
    public class RegisterForm
    {
        [Required]
        [Display(Name = "Your First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Your Last Name")]
        public string LastName { get; set; } = string.Empty;

        public string? Company { get; set; } 

        [Required]
        [EmailAddress]
        [Display(Name = "Your E-mail Address")]
        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Your Password")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;


        public string StreetName { get; set; } = null!;

        public string PostalCode { get; set; } = null!;

        public string City { get; set; } = null!;

        public string UserRole { get; set; } = "User";

        public string? ReturnUrl { get; set; }

        public bool TermsAndAgreements { get; set; }

    }
}