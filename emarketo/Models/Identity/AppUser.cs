using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace emarketo.Models.Identity
{
    public class AppUser : IdentityUser
    {
        [Required]
        [ProtectedPersonalData]
        [Display(Name = "First name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [ProtectedPersonalData]
        [Display(Name = "Last name")]
        public string LastName { get; set; } = string.Empty;
    }
}
