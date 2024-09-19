using Latihan.Helper;
using Latihan.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Latihan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class GetDataController : ControllerBase
    {
        private GetAllDataRepository _getDataRepository;
        public GetDataController(GetAllDataRepository getAllDataRepository)
        {
            _getDataRepository = getAllDataRepository;
        }
        [HttpGet("employees")]
        public IActionResult GetAllDepartments()
        {
            var data = _getDataRepository.GetAllEmployee();

            if (data == null)
            {
                return ResponseHTTP.CreateResponse(200, "No data employees.");
            }
            else
            {
                return ResponseHTTP.CreateResponse(200, "Employees retrieved successfully.", data);
            }
        }
        [HttpGet("accounts")]
        public IActionResult GetAllAccounts()
        {
            var data = _getDataRepository.GetAllAccount();

            if (data == null)
            {
                return ResponseHTTP.CreateResponse(200, "No data accounts.");
            }
            else
            {
                return ResponseHTTP.CreateResponse(200, "Accounts retrieved successfully.", data);
            }
        }

        [HttpGet("educations")]
        public IActionResult GetAllEducation()
        {
            var data = _getDataRepository.GetAllEducation();

            if (data == null)
            {
                return ResponseHTTP.CreateResponse(200, "No data universities.");
            }
            else
            {
                return ResponseHTTP.CreateResponse(200, "Universities retrieved successfully.", data);
            }
        }

        [HttpGet("profilings")]
        public IActionResult GetAllProfiling()
        {
            var data = _getDataRepository.GetAllProfiling();
            if (data == null)
            {
                return ResponseHTTP.CreateResponse(404, "No data profiling");
            }
            else
            {
                return ResponseHTTP.CreateResponse(200, "Profilings retrieved successfully.", data);
            }
        }
    }
}
