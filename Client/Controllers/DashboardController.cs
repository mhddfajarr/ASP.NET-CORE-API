using Client.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [SessionAuthorize("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult frontEnd()
        {
            return View();
        }
    }
}
