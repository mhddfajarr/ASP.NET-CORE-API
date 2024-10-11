using Latihan.Models;
using Latihan.Repositories;
using Latihan.ViewModels;
using Latihan.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Latihan.Context;

namespace Latihan.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class AccountController : ControllerBase
    {
        private AccountRepository _accountRepository;
        private GetAllDataRepository _getDataRepository;
        private IConfiguration _configuration;

        public AccountController(AccountRepository accountRepository, IConfiguration configuration, GetAllDataRepository getDataRepository)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
            _getDataRepository = getDataRepository;
        }

        [HttpGet("empData")]
        //[Authorize(Roles = "Admin")]
        public IActionResult GetAllDataAccount()
        {
            var data = _accountRepository.GetAllEmpData();

            if (data == null)
            {
                return ResponseHTTP.CreateResponse(200, "No data account.");
            }
            else
            {
                return ResponseHTTP.CreateResponse(200, "Data Account retrieved successfully.", data);
            }
        }
       

        [HttpPost("login")]
        public IActionResult Login(AuthVM.LoginVM login)
        {
            

            var account = _accountRepository.Login(login);
            if (account == 1)
            {
                var employeeData = _getDataRepository.GetEmployeeByNIKOrEmail(login.NIKOrEmail);

                var claims = new List<Claim>
            {
                new Claim("nik",employeeData.NIK),
                new Claim("fullname", employeeData.FirstName + " " + employeeData.LastName),
            };
                var roles = _getDataRepository.GetRoles(login.NIKOrEmail);
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:API"],
                    claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: signIn
                    );
                var tokenResult = new JwtSecurityTokenHandler().WriteToken(token);

                var data = new
                {
                    token = tokenResult
                };
                return ResponseHTTP.CreateResponse(200, "Login successful.", data);
            }
            else if (account == -1)
            {
                return ResponseHTTP.CreateResponse(400, "Invalid password.");
            }
            else
            {
                return ResponseHTTP.CreateResponse(404, "NIK or email not found.");
            }
        }

        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public IActionResult Register(AuthVM.RegisterVM registerVM)
        {
            var checkEmail = _accountRepository.CheckEmail(registerVM.Email);

            if (checkEmail)
            {
                return ResponseHTTP.CreateResponse(400, "Email has been used");
            }
            var data = _accountRepository.Register(registerVM);

            if (data > 0)
            {
                return ResponseHTTP.CreateResponse(200, "Success add new data.", registerVM);
            }
            else
            {
                return ResponseHTTP.CreateResponse(400, "Fail to add new data.");
            }
        }


    }
}
