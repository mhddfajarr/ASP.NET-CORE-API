using Latihan.Helper;
using Latihan.Models;
using Latihan.Repositories;
using Microsoft.AspNetCore.Http;
using Latihan.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Latihan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private ProfileRepository _profileRepository;
        public ProfileController(ProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }
        [HttpGet("{nik}")]
        [Authorize]
        public IActionResult GetProfile(string nik)
        {
            var data = _profileRepository.GetProfile(nik);

            if (data == null)
            {
                return ResponseHTTP.CreateResponse(200, "No data profile.");
            }
            else
            {
                return ResponseHTTP.CreateResponse(200, "Data profile retrieved successfully.", data);
            }
        }
        [Authorize]
        [HttpPut("{nik}")]
        public IActionResult UpdateProfile(string nik, DataVM.ProfileVM profile)
        {
            if (nik != profile.NIK)
            {
                return ResponseHTTP.CreateResponse(400, "Id url and Id Body not Same");
            }

            int data = _profileRepository.UpdateProfile(profile);
            
            if (data > 0)
            {
                return ResponseHTTP.CreateResponse(200, "Role successfully updated.", profile);
            }
            return ResponseHTTP.CreateResponse(404, "No role found with the sepecific id.");
        }
        [Authorize]
        [HttpPut("changePassword")]
        public IActionResult ChangePassword(DataVM.ChangePasswordVM changePasswordVM)
        {
            var result = _profileRepository.ChangePassword(changePasswordVM);
            if (result == 1)
            {
               
                return ResponseHTTP.CreateResponse(200, "Success change password.");
            }
            else if (result == -1)
            {
                return ResponseHTTP.CreateResponse(400, "Invalid old password.");
            }
            else
            {
                return ResponseHTTP.CreateResponse(404, "NIK  not found.");
            }
        }
    }
}
