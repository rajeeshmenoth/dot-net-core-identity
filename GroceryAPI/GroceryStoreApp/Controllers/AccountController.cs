using GroceryStoreApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> TwoFactorAuthentication(string userName, bool rememberMe, string returnUrl = null)
        {
            var user = await _userManager.FindByEmailAsync(userName);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid user name.");
                return View();
            }
            var providers = await _userManager.GetValidTwoFactorProvidersAsync(user);
            if (!providers.Contains("Authenticator"))
            {
                ModelState.AddModelError("", "Invalid provider.");
                return View();
            }
            var token = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider);
            ViewData["ReturnUrl"] = returnUrl + $"?{token}";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TwoFactorAuthentication(TwoFactorModel twoFactorModel, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(twoFactorModel);
            }
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid authentication code.");
                return RedirectToAction();
            }
            var result = await _signInManager.TwoFactorSignInAsync(TokenOptions.DefaultPhoneProvider, twoFactorModel.TwoFactorAuthCode, twoFactorModel.RememberMe, rememberClient: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "The account is locked out");
                return View();
            }
            else
            {
                ModelState.AddModelError("", "Invalid Login Attempt");
                return View();
            }
        }
    }
}
