using AutoMapper;
using emarketo.Models.Forms;
using emarketo.Models.Identity;
using emarketo.ViewModels.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fixxo.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthenticationController(IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        [Route("/register")]
        public IActionResult Register(string ReturnUrl = null!)
        {
            var viewModel = new RegisterViewModel
            {
                Form = new RegisterForm(),
                ReturnUrl = ReturnUrl ?? Url.Content("~/")
            };

            return View(viewModel);
        }


        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (!await _userManager.Users.AnyAsync() || !await _roleManager.Roles.AnyAsync())
                {
                    try
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Administrator"));
                        viewModel.Form.UserRole = "Administrator";
                    }
                    catch { }

                    try { await _roleManager.CreateAsync(new IdentityRole("User Manager")); } catch { }
                    try { await _roleManager.CreateAsync(new IdentityRole("Product Manager")); } catch { }
                    try { await _roleManager.CreateAsync(new IdentityRole("User")); } catch { }


                }

                var appUser = _mapper.Map<AppUser>(viewModel.Form);
                appUser.UserName = appUser.Email;

                var result = await _userManager.CreateAsync(appUser, viewModel.Form.Password);
                if (result.Succeeded)
                {
                    // add user to role
                    await _userManager.AddToRoleAsync(appUser, viewModel.Form.UserRole);

                    var signInResult = await _signInManager.PasswordSignInAsync(appUser, viewModel.Form.Password, false, false);
                    if (signInResult.Succeeded)
                        return LocalRedirect(viewModel.ReturnUrl);
                    else
                        return RedirectToAction("Login");
                }

                ModelState.AddModelError(string.Empty, "Unable to create an Account. Please contact customer support.");
            }

            return View(viewModel);

        }




        [Route("/login")]
        public IActionResult Login(string ReturnUrl = null!)
        {
            var viewModel = new LoginViewModel
            {
                Form = new LoginForm(),
                ReturnUrl = ReturnUrl ?? Url.Content("~/")
            };

            return View(viewModel);
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(viewModel.Form.Email, viewModel.Form.Password, false, false);
                if (result.Succeeded)
                    return LocalRedirect(viewModel.ReturnUrl);
            }

            ModelState.AddModelError(string.Empty, "Incorrect email or password");
            return View(viewModel);

        }

        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            if (_signInManager.IsSignedIn(User))
                await _signInManager.SignOutAsync();

            return LocalRedirect("/");
        }

    }
}