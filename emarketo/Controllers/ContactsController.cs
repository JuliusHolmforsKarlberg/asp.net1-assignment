using emarketo.Models.Forms;
using Microsoft.AspNetCore.Mvc;

namespace emarketo.Controllers
{
    public class ContactsController : Controller
    {
        public string? ReturnUrl { get; private set; }

        public IActionResult Index(string returnUrl = null!)
        {
            var form = new ContactForm { ReturnUrl = ReturnUrl ?? Url.Content("~/") };
            return View(form);
        }

        [HttpPost]
        public IActionResult Index(ContactForm form)
        {
            return View();
        }
    }
}
