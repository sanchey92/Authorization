using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Basics.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public ActionResult Index() => View();

        [AllowAnonymous]
        public ActionResult Login(string returnUrl) => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var claims = new List<Claim> {new Claim("Demo", "Value")};
            var claimIdentity = new ClaimsIdentity(claims, "Cookie");
            var claimPrincipals = new ClaimsPrincipal(claimIdentity);

            await HttpContext.SignInAsync("Cookie", claimPrincipals);
            
            return Redirect(model.ReturnUrl);
        }

        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync("Cookie");
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