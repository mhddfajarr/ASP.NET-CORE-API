using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class ChangePasswordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
