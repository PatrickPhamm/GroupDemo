using Microsoft.AspNetCore.Mvc;

namespace KoiShowManagementSystem.MVCWebApp.Controllers
{
    public class ApplicationsController : Controller
    {
        public IActionResult AjaxApplicationIndex()
        {
            return View();
        }
    }
}
