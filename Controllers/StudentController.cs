using AutoMapper;
using CollegeApp.Data;
using CollegeApp.Data.Repository;
using CollegeApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;

        public StudentController(ILogger<StudentController> logger, IMapper mapper, IStudentRepository studentRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _studentRepository = studentRepository;
        }

        [HttpGet]
        [Route("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {

            _logger.LogInformation("GetStudents method started");
            var students = await _studentRepository.GetAllAsync();
            var studentDTOData = _mapper.Map<List<StudentDTO>>(students);
            return Ok(studentDTOData);
            //var students = await _dbcontext.Students.Select(s => new StudentDTO()
            //{
            //    Id = s.Id,
            //    StudentName = s.StudentName,
            //    Address = s.Address,
            //    Email = s.Email
            //}).ToListAsync();//using linqQ
            //return Ok(_dbcontext.Students);
        }
        [HttpGet]
        [Route("{id:int}", Name = "GetStudentsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO>> GetStudentsByIdAsync(int id)
        {
            //BadRequest -400-BadRequest-client Error
            if (id <= 0)
            {
                _logger.LogWarning("Bad request");
                return BadRequest();
            }
            var student = await _studentRepository.GetByIdAsync(student => student.Id == id);
            //NotFound-404-Not Found-client Error
            if (student == null)
            {
                _logger.LogError("Student not found with given id");
                return NotFound($"The Student with id {id}not found");
            }
            var studentDTOData = _mapper.Map<StudentDTO>(student);
            return Ok(studentDTOData);
            //var studentDTO = new StudentDTO
            //{
            //    Id = student.Id,
            //    StudentName = student.StudentName,
            //    Address = student.Address,
            //    Email = student.Email
            //};
            ////Ok-200-Success
            ////return Ok(CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault());
            //return Ok(studentDTO);
        }

        [HttpGet]
        [Route("{name:alpha}", Name = "GetStudentsByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Student>> GetStudentsByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                //BadRequest -400-BadRequest-client Error
                return BadRequest();
            var student = await _studentRepository.GetByNameAsync(student => student.StudentName.ToLower().Contains(name));

            //NotFound-404-Not Found-client Error
            if (student == null)
                return NotFound($"The Student with name {name}not found");
            var studentDTO = _mapper.Map<StudentDTO>(student);
            return Ok(studentDTO);
            //return Ok(await _dbcontext.Students.Where(n => n.StudentName == name).FirstOrDefaultAsync());
        }
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO>> CreateStudentAsync([FromBody] StudentDTO dto)
        {
            //if(!ModelState.IsValid)
            //    return BadRequest(ModelState);//if we not use ApiController in above
            //                                  //this validation will work
            //                                  //if we use ApiController in above
            //                                  //by default validation will work
            if (dto == null)
                return BadRequest();

            //if(model.AdmitionDate<DateTime.Now)
            //{
            //    //1.Directly Adding error messages to model state
            //    //2.Using Custom Attribute
            //    ModelState.AddModelError("AdmissionDate Error", "Admission date must be greter than or equal to the today date");
            //    return BadRequest(ModelState);
            //}

            //Student student = new Student
            //{
            //    StudentName = model.StudentName,
            //    Address = model.Address,
            //    Email = model.Email
            //};
            //instead of writing above six lines of code we can simply write 
            //Below lines of code using automapper
            Student student = _mapper.Map<Student>(dto);
            var studentAfterCreation = await _studentRepository.CreateAsync(student);
            dto.Id = studentAfterCreation.Id;
            return CreatedAtRoute("GetStudentsById", new { id = dto.Id }, dto);
            //return Ok(model);

        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateStudentAsync([FromBody] StudentDTO dto)
        {
            if (dto == null || dto.Id <= 0)
                BadRequest();
            //var existingstudent = await _dbcontext.Students.AsNoTracking().Where(s => s.Id == dto.Id).FirstOrDefaultAsync();
            var existingstudent = await _studentRepository.GetByIdAsync(student => student.Id == dto.Id, true);
            if (existingstudent == null)
                return NotFound();
            //var newRecord = new Student()
            //{
            //    Id = existingstudent.Id,
            //    StudentName = model.StudentName,
            //    Email = model.Email,
            //    Address = model.Address,
            //};
            //instead of writing above six lines of code we can simply write 
            //Below lines of code using automapper
            Student newRecord = _mapper.Map<Student>(dto);
            await _studentRepository.UpdateAsync(newRecord);
            return NoContent();
            //existingstudent.StudentName = model.StudentName;
            //existingstudent.Email = model.Email;
            //existingstudent.Address = model.Address;
            //_dbcontext.SaveChanges();
            //return NoContent();

        }

        [HttpPut]
        [Route("{id:int}/UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateStudentPartialAsync(int id, [FromBody] JsonPatchDocument<StudentDTO> patchdocument)
        {
            if (patchdocument == null || id <= 0)
                BadRequest();
            var existingstudent = await _studentRepository.GetByIdAsync(student => student.Id == id, true);
            if (existingstudent == null)
                return NotFound();
            //var studentDTO = new StudentDTO
            //{
            //    Id = existingstudent.Id,
            //    StudentName = existingstudent.StudentName,
            //    Address = existingstudent.Address,
            //    Email = existingstudent.Email
            //};
            var studentDTO = _mapper.Map<StudentDTO>(existingstudent);

            patchdocument.ApplyTo(studentDTO, ModelState);
            if (!ModelState.IsValid)
                return BadRequest();
            existingstudent = _mapper.Map<Student>(studentDTO);
            //existingstudent.StudentName = studentDTO.StudentName;
            //existingstudent.Email = studentDTO.Email;
            //existingstudent.Address = studentDTO.Address;
            await _studentRepository.UpdateAsync(existingstudent);
            return NoContent();

        }


        [HttpDelete("{id}", Name = "DeleteStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteStudentAsync(int id)
        {
            if (id <= 0)
                //BadRequest -400-BadRequest-client Error
                return BadRequest();
            var student = await _studentRepository.GetByIdAsync(student => student.Id == id);
            //NotFound-404-Not Found-client Error
            if (student == null)
                return NotFound($"The Student with id {id}not found");
            _studentRepository.DeleteAsync(student);
            return true;
        }
    }
}
