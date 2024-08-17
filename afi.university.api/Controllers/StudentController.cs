using afi.university.application.Common.Exceptions;
using afi.university.application.Services.Interfaces;
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
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;
        private readonly ILogger<CourseController> _logger;

        public StudentController(IStudentService studentService, ICourseService courseService, ILogger<CourseController> logger)
        {
            this._studentService = studentService;
            this._courseService = courseService;
            this._logger = logger;
        }

        /// <summary>
        /// Get all students
        /// </summary>
        /// <returns></returns>
        
        [HttpGet]
        //[Authorize(Roles = "Admin, Lecture")]
        public async Task<ActionResult<IEnumerable<StudentResponse>>> GetAllStudents()
        {
            IEnumerable<StudentResponse> students;
            try
            {
                students = await _studentService.GetAllStudentsAsync();                
            }
            catch(NotFoundException ex)
            {
                _logger.LogWarning("Failed getting list of students {0} - ", ex.Message);
                return NoContent();
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
        public async Task<ActionResult<StudentResponse>> GetStudentCourses(Guid id)
        {
            StudentResponse student;
            try
            {
                StudentRequest studentRequest = new StudentRequest(id);
                student = await _studentService.GetStudentCoursesAsync(studentRequest);
            }
            catch (StudentNotFoundException ex)
            {
                _logger.LogWarning("Failed getting student {0} - ", ex.Message);
                return NotFound(ex.Message);
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
        //[AllowAnonymous]
        //[HttpPost]
        //public async Task<ActionResult<StudentRegistrationResponseDto>> RegisterStudent(StudentRegistrationRequestDto studentRegistration)
        //{
        //    StudentRegistrationResponseDto response = new();
        //    try
        //    {
        //        response = await _studentService.RegisterToUniversityAsync(studentRegistration);
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        var tst = ex;
        //    }

        //    if (response == null) return BadRequest();

        //    return Ok(response);
        //}

        /// <summary>
        /// Registers a student to a course/courses
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="studentCourses"></param>
        /// <returns></returns>
        //[Authorize(Roles = "Student")]
        //[HttpPost(Name = "RegisterCourse")]
        //public async Task<ActionResult<bool>> RegisterCourse(CourseRegistrationRequestDto courseRegistration)
        //{
        //    bool response;
        //    try
        //    {
        //        response = await _studentService.RegisterCourseAsync(courseRegistration);
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }        

        //    return Ok(response);
        //}





        /// <summary>
        /// Gets all University courses
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles = "Student")]
        //[HttpGet(Name = "GetAllAvailableCourses")]
        //public async Task<ActionResult<List<StudentCoursesDto>>> GetAllAvailableCourses()
        //{
        //    List<StudentCoursesDto> universityCourses;
        //    try
        //    {
        //        universityCourses = await _courseService.GetAllCoursesAsync();
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }


        //    if (universityCourses == null) return BadRequest();

        //    return Ok(universityCourses);
        //}


        /// <summary>
        /// Un-registers from a course/course
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="studentCourses"></param>
        /// <returns></returns>
        //[Authorize(Roles = "Student")]
        //[HttpPost(Name = "UnregisterFromACourse")]
        //public async Task<ActionResult<bool>> UnregisterFromACourse(CourseRegistrationRequestDto courseRegistration)
        //{
        //    bool response;
        //    try
        //    {
        //        response = await _studentService.UnregisterAsync(courseRegistration);
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //    return Ok(response);
        //}


        /// <summary>
        /// Gets all university students
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles = "Admin")]
        //[HttpGet(Name = "GetAllUniversityStudents")]
        //public async Task<ActionResult<List<StudentsResponseDto>>> GetAllUniversityStudents()
        //{
        //    List<StudentsResponseDto> response;
        //    try
        //    {
        //        response = await _studentService.GetAllUniversityStudentsAsync();
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //    return Ok(response);
        //}
    }
}
