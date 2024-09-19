using Latihan.Models;
using Latihan.Repositories;
using Latihan.ViewModels;
using Latihan.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace Latihan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class AccountController : ControllerBase
    {
        private AccountRepository _accountRepository;

        public AccountController(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet("empData")]
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
                return ResponseHTTP.CreateResponse(200, "Login successful.");
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
