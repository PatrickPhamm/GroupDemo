using Microsoft.AspNetCore.Mvc;

namespace KoiShowManagementSystem.MVCWebApp.Controllers
{
    public class ContestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
