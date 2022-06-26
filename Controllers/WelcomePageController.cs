using Microsoft.AspNetCore.Mvc;

namespace Resources.Controllers
{
    public class WelcomePageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
