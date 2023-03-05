using Microsoft.AspNetCore.Mvc;

namespace emarketo.Controllers
{
    public class AboutController : Controller
    {
        [Route("/contacts")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Come in Contact with Us";
            return View();
        }
    }
}