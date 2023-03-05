using emarketo.Models.Forms;

namespace emarketo.ViewModels.Authentication
{
    public class RegisterViewModel
    {
        public RegisterForm Form { get; set; } = null!;
        public string ReturnUrl { get; set; } = null!;
    }
}
