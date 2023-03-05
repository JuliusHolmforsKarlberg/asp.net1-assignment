using emarketo.Models.Forms;

namespace emarketo.ViewModels.Authentication
{
    public class LoginViewModel
    {
        public LoginForm Form { get; set; } = null!;
        public string ReturnUrl { get; set; } = null!;
    }
}