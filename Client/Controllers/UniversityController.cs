using Client.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Client.Controllers
{
    [SessionAuthorize("Admin")]
    public class UniversityController : Controller
    {
        public IActionResult Index()
        {
            return View();  
        }
    }
}
