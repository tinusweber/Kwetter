using Microsoft.AspNetCore.Mvc;
using profileService.Service;

namespace profileService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository) =>
            (_studentRepository) = (studentRepository);


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_studentRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var student = _studentRepository.Get(id);
            if (student is null)
                return NotFound();

            return Ok(student);
        }
    }
}
