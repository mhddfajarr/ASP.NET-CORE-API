using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class UniversityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
