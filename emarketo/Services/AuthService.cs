using emarketo.Models.Forms;
using emarketo.Models.Identity;
using emarketo.Models;
using emarketo.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace emarketo.Services
{
    public class AuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IdentityContext _context;
        private readonly DataContext _context;

        public AuthService(DataContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> CreateUserAsync(SignUpForm form)
        {
            if (await _context.Users.AnyAsync(x => x.Email == form.Email))
                return new ConflictResult();

            var identityUser = new IdentityUser
            {
                UserName = form.Email,
                Email = form.Email
            };

            var result = await _userManager.CreateAsync(identityUser, form.Password);
            if (result.Succeeded)
                return new OkResult();

            return new BadRequestResult();
        }

        public async Task<IActionResult> LoginUserAsync(SignInForm form)
        {
            if (await _context.Users.AnyAsync(x => x.Email == form.Email))
            {
                var result = await _signInManager.PasswordSignInAsync(form.Email, form.Password, false, false);
                if (result.Succeeded)
                    return new OkResult();
            }

            return new BadRequestResult();
        }


        public async Task SigOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> RegisterAsync(RegisterForm form)
        {
            var identityUser = new IdentityUser { 
                UserName = form.Email, 
                Email = form.Email, 
                PhoneNumber = form.PhoneNumber 
            };

            var identityProfile = new IdentityUserProfile
            {
                UserId = identityUser.Id,
                FirstName = form.FirstName,
                LastName = form.LastName,
                StreetName = form.StreetName,
                PostalCode= form.PostalCode,
                City = form.City,
                Company = form.Company
            };

            var result = await _userManager.CreateAsync(identityUser, form.Password);
            if (result.Succeeded)
            {
                _context.UserProfiles.Add(identityProfile);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> LoginAsync(LoginForm form, bool keepMeLoggedIn = false)
        {
            var result = await _signInManager.PasswordSignInAsync(form.Email, form.Password, keepMeLoggedIn, false);
            return result.Succeeded;
        }
    }
}
