using Microsoft.AspNetCore.Mvc;

namespace LiderApp.WebUI.Areas.Admin
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
