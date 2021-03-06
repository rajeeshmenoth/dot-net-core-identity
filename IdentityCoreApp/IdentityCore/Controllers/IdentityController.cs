using IdentityCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityCore.Controllers
{
    public class IdentityController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public IdentityController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Signup()
        {
            var model = new SignupViewModel();
            model.Country = "--Select--";
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup(SignupViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(!await _roleManager.RoleExistsAsync(model.Role))
                {
                    var role = new IdentityRole { Name = model.Role };
                    var response = await _roleManager.CreateAsync(role);
                    if(!response.Succeeded)
                    {
                        var error = response.Errors.Select(x => x.Description);
                        ModelState.AddModelError("Role", string.Join(",", error));
                        return BadRequest(model);
                    }
                }

                if (await _userManager.FindByEmailAsync(model.Email) == null)
                {
                    var user = new IdentityUser
                    {

                        Email = model.Email,
                        UserName = model.Email,
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var userClaim = new Claim("Country", model.Country);
                        await _userManager.AddClaimAsync(user, userClaim);
                        await _userManager.AddToRoleAsync(user, model.Role);
                        return RedirectToAction("Signin");
                    }

                    ModelState.AddModelError("Signup", string.Join("", result.Errors.Select(x => x.Description)));
                    return View(model);
                }
            }

            return View(model);
        }

        public IActionResult Signin()
        {
            return View(new SigninViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signin(SigninViewModel model)
        {
            string returnUrl = "https://localhost:44336/";
            if (ModelState.IsValid)
            {
                var signinUser = await _userManager.FindByEmailAsync(model.Username);

                if (signinUser == null)
                {
                    ModelState.AddModelError("Login", "Invalid login attempt");
                    return View(model);
                }
                var result = await _signInManager.PasswordSignInAsync(signinUser.UserName, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    // Added user policy in middleware
                    //var userClaims = await _userManager.GetClaimsAsync(signinUser);

                    //if (userClaims.Any(x => x.Type != "Country"))
                    //{
                    //    return View(model);
                    //}

                    return RedirectToAction("Index", "Home");
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction("TwoFactorAuthentication", "Account", new { model.Username, model.RememberMe, returnUrl });
                }
                else
                {
                    ModelState.AddModelError("Login", "There is an issue in login attempt");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Signin");
        }

        [Authorize]
        public async Task<IActionResult> MfaConfiguration()
        {
            const string provider = "aspnetidentity";
            var user = await _userManager.GetUserAsync(User);
            await _userManager.ResetAuthenticatorKeyAsync(user);
            var token = await _userManager.GetAuthenticatorKeyAsync(user);
            var qrCodeUrl = $"otpauth://totp/{provider}:{user.Email}?secret={token}&issuer={provider}&digits=6";
            var mfa = new MFAViewModel { MfaToken = token , QrCodeUrl = qrCodeUrl };
            return View(mfa);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MfaConfiguration(MFAViewModel mfa)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var result = await _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider, mfa.MfaCode);
                if (result)
                {
                    var auth = await _userManager.SetTwoFactorEnabledAsync(user,true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Validate", "Your MFA code is invalid");
                }               
            }
            return View(mfa);
        }
        public async Task<IActionResult> AccessDenied(SignupViewModel model)
        {
            return View(model);
        }
    }
}
