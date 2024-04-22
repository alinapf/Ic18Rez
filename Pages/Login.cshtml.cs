using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PasechnikovaPR33p18.Models;
using PasechnikovaPR33p18.Services;
using PasechnikovaPR33p18.ViewModels;
using System.Security.Claims;

namespace PasechnikovaPR33p18.Pages
{
    public class LoginModel :PageModel
    {
        private readonly IUserService userService;

        public LoginViewModel LoginViewModel { get; set; } = null!;

        public LoginModel (IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult OnGet ()
        {
            return Page( );
        }
        private async Task LogIn (User user)
        {
            List<Claim> userClaims = [ new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString( )), new Claim(ClaimTypes.Name, user.Username), new Claim(ClaimTypes.Role, "Client") ];

            var identity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);
        }

        public async Task<IActionResult> OnPost (LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Page( );
            }

            bool isValidCredentials = userService.CheckPassword(loginViewModel.Username, loginViewModel.Password);

            if (!isValidCredentials)
            {
                ModelState.AddModelError("", "Неверное имя пользователя или пароль");
                return Page( );
            }

            User user = userService.GetUser(loginViewModel.Username)!;
            await LogIn(user);
            return Page( );
        }
    }
}
