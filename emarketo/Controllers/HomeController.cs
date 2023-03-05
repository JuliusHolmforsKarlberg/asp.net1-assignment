using emarketo.Services;
using emarketo.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace emarketo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShowcaseService _showcaseService;

        public HomeController(ShowcaseService showcaseService)
        {
            _showcaseService = showcaseService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel();
            viewModel.Showcase = await _showcaseService.GetLatest();

            return View(viewModel);
        }
    }
}