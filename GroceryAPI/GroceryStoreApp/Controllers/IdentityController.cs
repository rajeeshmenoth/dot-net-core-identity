using GroceryStoreApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreApp.Controllers
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
            if (ModelState.IsValid)
            {
                var signedUser = await _userManager.FindByEmailAsync(model.Username);
                var result = await _signInManager.PasswordSignInAsync(signedUser.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return Redirect("https://localhost:5001/Fruits/GetFruits");
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
        public async Task<IActionResult> AccessDenied(SignupViewModel model)
        {
            return View(model);
        }
    }
}
