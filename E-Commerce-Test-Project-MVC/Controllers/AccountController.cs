using E_Commerce_Test_Project_MVC.Helpers.Enums;
using E_Commerce_Test_Project_MVC.Models;
using E_Commerce_Test_Project_MVC.ViewModels.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc; 
namespace E_Commerce_Test_Project_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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
            await _userManager.AddToRoleAsync(user, Roles.Member.ToString());
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var url = Url.Action("ConfirmEmail","Account",new {userId=user.Id,token},Request.Scheme,Request.Host.ToString());


            //var email = new MimeMessage();
            //email.From.Add(MailboxAddress.Parse("nurane.ismayilzade16@gmail.com"));
            //email.To.Add(MailboxAddress.Parse(user.Email));
            //email.Subject = "Test Email Subject";
            //string html = $"<a href='{url}'>Click here for confirm!</a>";
            //email.Body = new TextPart(TextFormat.Html) { Text = html };


            //using var smtp = new SmtpClient();
            //smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            //smtp.Authenticate("nurane.ismayilzade16@gmail.com", "salam");
            //smtp.Send(email);
            //smtp.Disconnect(true);


            return RedirectToAction(nameof(VerifyEmail));
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.ConfirmEmailAsync(user, token);
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult VerifyEmail()
        {
            return View();
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
                user = await _userManager.FindByNameAsync(request.EmailOrUsername);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> CreateRoles()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(
                        new IdentityRole { Name = role.ToString() });
                }
            }
            return Ok();
        }



    }
}
