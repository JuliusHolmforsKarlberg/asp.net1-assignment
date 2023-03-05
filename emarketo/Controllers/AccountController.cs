using emarketo.Services;
using emarketo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using emarketo.Models.Forms;

namespace emarketo.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult SignUp(string ReturnUrl = null!)
        {
            var form = new SignUpForm();
            form.ReturnUrl = ReturnUrl ?? Url.Content("/");

            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpForm form)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.CreateUserAsync(form);
                if (result is OkResult)
                    return LocalRedirect(form.ReturnUrl);
                else if (result is ConflictResult)
                    ModelState.AddModelError("", "User with the same email already exists");
                else
                    ModelState.AddModelError("", "An unexpected error occured!");
            }

            return View(form);
        }

        public IActionResult SignIn(string ReturnUrl = null!)
        {
            var form = new SignInForm();
            form.ReturnUrl = ReturnUrl ?? Url.Content("/");

            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInForm form)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.LoginUserAsync(form);
                if (result is OkResult)
                    return LocalRedirect(form.ReturnUrl);
                else
                    ModelState.AddModelError("", "Incorrect email or password");
            }

            return View(form);
        }

        public async Task <IActionResult> Index(string id)
        {
            var userAccount = await _userService.GetUserAccountAsync(id);
            return View(userAccount);
        }

        public async Task<IActionResult> SignOut()
        {
            await _authService.SigOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}