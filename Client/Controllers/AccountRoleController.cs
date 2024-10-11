using Client.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [SessionAuthorize("Admin")]
    public class AccountRoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
