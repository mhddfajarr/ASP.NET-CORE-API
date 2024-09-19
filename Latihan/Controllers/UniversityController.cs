using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Latihan.Models;
using Latihan.Repositories;
using Latihan.Helper;
using Microsoft.AspNetCore.Cors;


namespace Latihan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UniversityController : ControllerBase
    {
        private UniversityRepository _universityRepository;
        public UniversityController(UniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }
        
        [HttpGet]
        public IActionResult GetAllUniversities()
        {
            var data = _universityRepository.GetAllUniversities();

            if (data == null)
            {
                return ResponseHTTP.CreateResponse(200, "No data universities.");
            }
            else
            {
                return ResponseHTTP.CreateResponse(200, "Universities retrieved successfully.", data);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetDepartmentsById(string id)
        {
            var data = _universityRepository.GetUniveristyById(id);

            if (data == null)
            {
                return ResponseHTTP.CreateResponse(404, "No university found with the sepecific id.");
            }
            return ResponseHTTP.CreateResponse(200, "Univeristy retrieved successfully.", data);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDepartment(string id, [FromBody] University university)
        {
            if (id != university.Id)
            {
                return ResponseHTTP.CreateResponse(400, "Id url and Id Body not Same");
            }

            int data = _universityRepository.UpdateUniversity(university);

            if (data > 0)
            {
                return ResponseHTTP.CreateResponse(200, "University successfully updated.", university);
            }
            return ResponseHTTP.CreateResponse(404, "No university found with the sepecific id.");
        }




        [HttpPost]
        public IActionResult AddUniversity(University university)
        {
            var addDepartment = _universityRepository.AddUniversity(university);

            if (addDepartment > 0)
            {
                return ResponseHTTP.CreateResponse(201, "Success add new University.", university);
            }
            else
            {
                return ResponseHTTP.CreateResponse(404, "Failed added new university.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(string id)
        {
            int result = _universityRepository.DeleteUniversity(id);
            if (result > 0)
            {
                return ResponseHTTP.CreateResponse(200, "Success deleted university.");
            }
            return ResponseHTTP.CreateResponse(404, "No university found with the sepecific id.");
        }

    }
}
