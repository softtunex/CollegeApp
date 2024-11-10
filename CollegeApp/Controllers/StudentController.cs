using CollegeApp.Data;
using CollegeApp.Modal;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController: ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly CollegeDb _dbContext;
        public StudentController(ILogger<StudentController> logger, CollegeDb dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
                
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            _logger.LogInformation("Get Students method started exe");

            //var students = new List<StudentDTO>();

            //foreach (var item in _dbContext.Students)
            //{
            //    StudentDTO obj = new StudentDTO()

            var students = _dbContext.Students.Select(item => new StudentDTO()
            {
                Id = item.Id,
                StudentName = item.StudentName,
                Address = item.Address,
                StudentEmail = item.Email,
            }).ToList();
                //students.Add(obj);
            //}
            //Ok - 200 - success
            return Ok(students);
        }

        [HttpGet("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            var student = _dbContext.Students.Where(n => n.Id == id).FirstOrDefault();
            //Ok - 400 - BadRequest

            if (id <= 0)
            {
                _logger.LogWarning("Bad Request");
                return BadRequest();
            }
            if (student == null)
            {
                _logger.LogError("Student not found with a given Id"  );
                return NotFound();
            }
            var studentDTO = new StudentDTO()
            {
                Id=student.Id,
                StudentName = student.StudentName,
                Address = student.Address,
                StudentEmail = student.Email,
            };
            return Ok(studentDTO);
        }


        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<StudentDTO> GetStudentByName(string name)
        {
            var student = _dbContext.Students.Where(n => n.StudentName == name).FirstOrDefault();
            //Ok - 400 - BadRequest

            if (string.IsNullOrEmpty(name))
                BadRequest();
            if (student == null) return NotFound();
            var studentDTO = new StudentDTO()
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Address = student.Address,
                StudentEmail = student.Email,
                DOB = student.DOB,
            };
            return Ok(studentDTO);
        }
        
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    
        public ActionResult<bool> DeleteStudentById(int id)
        {
            var student = _dbContext.Students.Where(n=>n.Id == id).FirstOrDefault();
            if(student == null) return NotFound();
            _dbContext.Students.Remove(student);
            _dbContext.SaveChanges();

            return Ok(true);
        }

        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        
        public ActionResult<StudentDTO> CreateStudent([FromBody]StudentDTO model)
        {
            if (model == null) return BadRequest();

            Student student = new Student
            {
                Email = model.StudentEmail,
                StudentName = model.StudentName,
                Address = model.Address,
                DOB = model.DOB,
            };
            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();
            model.Id = student.Id;

            return CreatedAtRoute("GetStudentById", new {id = model.Id}, model);

            
        }


        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        
        public ActionResult<StudentDTO> UpdateStudent([FromBody]StudentDTO model)
        {
            if (model == null || model.Id <= 0) return BadRequest();

            var existingStudent = _dbContext.Students.Where(s=>s.Id == model.Id).FirstOrDefault();
            if (existingStudent == null) return NotFound();

            existingStudent.StudentName = model.StudentName;
            existingStudent.Address = model.Address;
            existingStudent.Email = model.StudentEmail;
            return Ok(existingStudent);

        }
        
        [HttpPatch("{id:int}/UpdateSingle")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        
        public ActionResult UpdateSingleStudentChange(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0) return BadRequest();

            var existingStudent = _dbContext.Students.Where(s=>s.Id == id).FirstOrDefault();
            if (existingStudent == null) return NotFound();

            var studentDTO = new StudentDTO
            {
                Id = existingStudent.Id,
                StudentEmail = existingStudent.Email,
                StudentName = existingStudent.StudentName,
                Address = existingStudent.Address,
                DOB = existingStudent.DOB,
            };

            patchDocument.ApplyTo(studentDTO, ModelState);

            if(!ModelState.IsValid) return BadRequest();

            existingStudent.StudentName = studentDTO.StudentName;
            existingStudent.Address = studentDTO.Address;
            existingStudent.Email = studentDTO.StudentEmail;
            _dbContext.Students.Add(existingStudent);
            return Ok(existingStudent);

        }
    
    }
}
