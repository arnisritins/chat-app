using System;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChatApp.Data.Entities;

namespace ChatApp.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// Sign In manager
        /// </summary>
        private readonly SignInManager<User> _signInManager;

        /// <summary>
        /// User manager
        /// </summary>
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Constructor
        /// </summary>
        public AccountController(UserManager<User> usrMng, SignInManager<User> sgnMng)
        {
            _userManager = usrMng;
            _signInManager = sgnMng;
        }

        /// <summary>
        /// Login page
        /// </summary>
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Chat");

            return View();
        }

        /// <summary>
        /// Login submit
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string provider)
        {
            var redirectUrl = Url.Action("LoginCallback");
            var properties = _signInManager
                .ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return Challenge(properties, provider);
        }

        /// <summary>
        /// Process login callback
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginCallback(string remoteError = null)
        {
            if (remoteError != null)
                throw new ApplicationException("Error from external provider.");

            // Get Facebook's user info
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
                throw new ApplicationException("Error loading external login information.");

            // Try to sign-in user
            var result = await _signInManager
                .ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false, true);

            if (!result.Succeeded)
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var user = new User { UserName = email, Email = email };

                // Create new user account
                var createResult = await _userManager.CreateAsync(user);

                if (createResult.Succeeded)
                {
                    var loginResult = await _userManager.AddLoginAsync(user, info);
                    if (loginResult.Succeeded)
                        await _signInManager.SignInAsync(user, false);
                }
            }

            return RedirectToAction("Index", "Chat");
        }

        /// <summary>
        /// Logout user
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
