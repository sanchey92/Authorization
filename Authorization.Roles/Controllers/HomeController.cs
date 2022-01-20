using Microsoft.AspNetCore.Mvc;

namespace Authorization.Roles.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Name = User?.Identity?.Name;
            ViewBag.IsAuthenticated = User?.Identity?.IsAuthenticated;
            return View();
        }

        public ActionResult AccessDenied() => View();
    }
}