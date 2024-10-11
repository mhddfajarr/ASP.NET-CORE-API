using Client.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [SessionAuthorize("Employee, Admin")]
    public class AccessDenied : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
