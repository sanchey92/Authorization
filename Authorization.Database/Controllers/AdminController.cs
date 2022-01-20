using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Authorization.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Database.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AdminController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
            => (_userManager, _signInManager) = (userManager, signInManager);

        public ActionResult Index() => View();

        [Authorize(Policy = "Administrator")]
        public ActionResult Administrator() => View();

        [Authorize(Policy = "Manager")]
        public ActionResult Manager() => View();

        [AllowAnonymous]
        public ActionResult Login(string returnUrl) => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                ModelState.AddModelError("UserName", "User Not Found!");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (result.Succeeded) return Redirect(model.ReturnUrl);

            return View(model);

        }

        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Home/Index");
        }
    }

    public class LoginViewModel
    {
        [Required] public string UserName { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string ReturnUrl { get; set; }
    }
}