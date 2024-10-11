using Latihan.Helper;
using Latihan.Models;
using Latihan.Repositories;
using Latihan.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Latihan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    [Authorize(Roles = "Admin")]

    public class RolesController : ControllerBase
    {
        private RoleRepository _roleRepository;
        public RolesController(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public IActionResult GetAllRoles() {
            var data = _roleRepository.GetAllRole();

            if (data == null)
            {
                return ResponseHTTP.CreateResponse(200, "No data Roles.");
            }
            else
            {
                return ResponseHTTP.CreateResponse(200, "Roles retrieved successfully.", data);
            }

        }
        [HttpDelete("{roleId}")]
        public IActionResult DeleteRole(string roleId)
        {
            int result = _roleRepository.DeleteRole(roleId);
            if (result > 0)
            {
                return ResponseHTTP.CreateResponse(200, "Success deleted role.");
            }
            return ResponseHTTP.CreateResponse(404, "No role found with the sepecific id.");
        }

        [HttpPost]
        public IActionResult AddRole(Role role)
        {
            var addRole = _roleRepository.AddRole(role);
            var data = role;

            if (addRole > 0)
            {
                return ResponseHTTP.CreateResponse(201, "Success add new role.", data);
            }
            else
            {
                return ResponseHTTP.CreateResponse(404, "Failed added new role.");
            }
        }

        [HttpPut("{roleId}")]
        public IActionResult UpdateRole(string roleId, [FromBody] Role role)
        {
            if (roleId != role.RoleId)
            {
                return ResponseHTTP.CreateResponse(400, "Id url and Id Body not Same");
            }

            int data = _roleRepository.UpdateRole(role);

            if (data > 0)
            {
                return ResponseHTTP.CreateResponse(200, "Role successfully updated.", role);
            }
            return ResponseHTTP.CreateResponse(404, "No role found with the sepecific id.");
        }

        [HttpGet("{roleId}")]
        public IActionResult GetUniversityById(string roleId)
        {
            var data = _roleRepository.GetRoleById(roleId);

            if (data == null)
            {
                return ResponseHTTP.CreateResponse(404, "No role found with the sepecific id.");
            }
            return ResponseHTTP.CreateResponse(200, "Role retrieved successfully.", data);
        }
    }
}
