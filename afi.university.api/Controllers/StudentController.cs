using afi.university.application.Common.Exceptions;
using afi.university.application.Services.Interfaces;
using afi.university.domain.Entities;
using afi.university.shared.DataTransferObjects.Requests;
using afi.university.shared.DataTransferObjects.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace afi.university.api.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {        
        private readonly IStudentService _studentService;
        private readonly ILogger<CourseController> _logger;

        public StudentController(IStudentService studentService, ILogger<CourseController> logger)
        {
            this._studentService = studentService;            
            this._logger = logger;
        }

        /// <summary>
        /// Get all students
        /// </summary>
        /// <returns></returns>
        
        [HttpGet]
        //[Authorize(Roles = "Admin, Lecture")]
        public async Task<ActionResult<IEnumerable<StudentResponse>>> GetAllStudentsAsync()
        {
            IEnumerable<StudentResponse> students;
            try
            {
                students = await _studentService.GetAllStudentsAsync();                
            }
            catch(NotFoundException ex)
            {
                _logger.LogWarning("Failed getting list of students {0} - ", ex.Message);
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed getting list of students {0} - ", ex);
                return BadRequest(ex.Message);
            }

           
            return (!students.Any()) ? NotFound() : Ok(students);
        }

        /// <summary>
        /// Get student courses
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>

        [HttpGet("{id:guid}")]
        //[Authorize(Roles = "Student")]
        public async Task<ActionResult<StudentResponse>> GetStudentCoursesAsync(Guid id)
        {
            StudentResponse student;
            try
            {
                StudentRequest studentRequest = new StudentRequest(id);
                student = await _studentService.GetStudentRegisteredCoursesAsync(studentRequest);
            }
            catch (StudentNotFoundException ex)
            {
                _logger.LogWarning("Failed getting student {0} - ", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed getting list of students {0} - ", ex);
                return BadRequest(ex.Message);
            }

            return (student == null) ? NotFound() : Ok(student);
        }

        /// <summary>
        /// Registers student to the University
        /// </summary>
        /// <param name="studentRegistration"></param>
        /// <returns></returns>
        
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ActionResult<RegistrationResponse>> RegisterStudentAsync([FromBody] RegistrationRequest registrationRequest)
        {
            RegistrationResponse response;
            try
            {
                response = await _studentService.RegisterStudentAsync(registrationRequest);
            }
            catch (StudentAlreadyExistsException ex)
            {
                _logger.LogWarning("Failed student registration {0} - ", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed student registration  {0} - ", ex);
                return BadRequest(ex.Message);
            }

            return (response == null) ? BadRequest() : Ok(response);
        }


        /// <summary>
        /// Registers a student to a course/courses
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="studentCourses"></param>
        /// <returns></returns>
        
        [HttpPost]
        [Route("enroll")]
        //[Authorize(Roles = "Student")]
        public async Task<ActionResult<bool>> EnrollCourseAsync([FromBody] CourseRegistrationRequest courseRegistrationRequest)
        {
            bool response;
            try
            {
                response = await _studentService.EnrollCourseAsync(courseRegistrationRequest);
            }
            catch (StudentNotFoundException ex)
            {
                _logger.LogWarning("Failed course registration {0} - ", ex.Message);
                return BadRequest(ex.Message); 
            }
            catch (CourseNotFoundException ex)
            {
                _logger.LogWarning("Failed course registration {0} - ", ex.Message);
                return BadRequest(ex.Message);
            } 
            catch (StudentAlreadyRegisteredForCourseException ex)
            {
                _logger.LogWarning("Failed course registration {0} - ", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed student registration  {0} - ", ex);
                return BadRequest(ex.Message);
            }

            return Ok(response);            
        }


        /// <summary>
        /// Un-registers from a course/course
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="studentCourses"></param>
        /// <returns></returns>
        
        [HttpPost]
        [Route("unenroll")]
        //[Authorize(Roles = "Student")]
        public async Task<ActionResult<bool>> UnregisterFromACourse([FromBody] CourseRegistrationRequest courseRegistrationRequest)
        {
            bool response;
            try
            {
                response = await _studentService.UnregisterAsync(courseRegistrationRequest);
            }
            catch (StudentNotFoundException ex)
            {
                _logger.LogWarning("Failed course de-registration {0} - ", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (CourseNotFoundException ex)
            {
                _logger.LogWarning("Failed course de-registration {0} - ", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed student de-registration  {0} - ", ex);
                return BadRequest(ex.Message);
            }

            return Ok(response);
        }

    }
}
