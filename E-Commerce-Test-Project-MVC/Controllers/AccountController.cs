using E_Commerce_Test_Project_MVC.Models;
using E_Commerce_Test_Project_MVC.ViewModels.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace E_Commerce_Test_Project_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Register(RegisterVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            AppUser user = new()
            {
                FullName = request.FullName,
                Email = request.Email,
                UserName = request.UserName
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(request);
            }
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Login","Account");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Login(LoginVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            AppUser user = await _userManager.FindByEmailAsync(request.EmailOrUsername);
            if (user is null)
            {
                user = await _userManager.FindByEmailAsync(request.EmailOrUsername);
            }
            if (user is null)
            {
                ModelState.AddModelError("", "Email,Username or password is wrong");
                return View(request);
            }
            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                ModelState.AddModelError("", "Email,Username or password is wrong");
                return View(request);
            }
            var response = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
            return RedirectToAction("Index", "Home");
        }
    }
}
