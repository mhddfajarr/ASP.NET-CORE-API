using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class SetSessionController : Controller
    {
        public IActionResult Index()
        {
            var rolesString = HttpContext.Session.GetString("Roles");

            if (rolesString == null)
            {
                return Content("No roles found in session.");
            }

            // Split the string back into an array
            var roles = rolesString.Split(',');

            // Use the roles as needed
            return Content(rolesString);
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserDataModel userData)
        {
            if (userData == null)
            {
                return BadRequest(new { message = "User data is null." });
            }

            // Check if Roles is a single string or an array
            if (string.IsNullOrEmpty(userData.Roles?.FirstOrDefault()))
            {
                return BadRequest(new { message = "Roles are missing." });
            }

            // If Roles is a single string, convert it to a list
            if (userData.Roles.Count == 1 && !string.IsNullOrEmpty(userData.Roles[0]))
            {
                userData.Roles = new List<string> { userData.Roles[0] }; 
            }

            var rolesString = string.Join(", ", userData.Roles);
            HttpContext.Session.SetString("Roles", rolesString);

            // Return success response
            return Ok(new { message = "User data processed successfully" });
        }


        [HttpGet]
        public IActionResult DeleteSession()
        {
            HttpContext.Session.Remove("Roles");
            //return Ok(new { message = "Session Roles deleted successfully." });
            return Content("berhasil deleteSession");
        }

        public class UserDataModel
        {
            public string Nik { get; set; }
            public string Fullname { get; set; }
            public List<string> Roles { get; set; }
        }
    }
}
