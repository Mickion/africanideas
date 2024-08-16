using afi.university.application.Models.Requests;
using afi.university.application.Models.Responses;
using afi.university.application.Services.Interfaces;
using afi.university.domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace afi.university.api.Controllers
{
    [Route("api/[controller]/[action]")]
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
        /// Registers student to the University
        /// </summary>
        /// <param name="studentRegistration"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost(Name = "RegisterToUniversity")]
        public async Task<ActionResult<StudentRegistrationResponseDto>> RegisterToUniversity(StudentRegistrationRequestDto studentRegistration)
        {
            StudentRegistrationResponseDto response=new();
            try
            {
                response = await _studentService.RegisterToUniversityAsync(studentRegistration);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                var tst = ex;
            }

            if (response == null) return BadRequest();

            return Ok(response);
        }

        /// <summary>
        /// Registers a student to a course/courses
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="studentCourses"></param>
        /// <returns></returns>
        [Authorize(Roles = "Student")]
        [HttpPost(Name = "RegisterCourse")]
        public async Task<ActionResult<bool>> RegisterCourse(CourseRegistrationRequestDto courseRegistration)
        {
            bool response;
            try
            {
                response = await _studentService.RegisterCourseAsync(courseRegistration);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }        

            return Ok(response);
        }


        /// <summary>
        /// Gets all student registered courses
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Student")]
        [HttpPost(Name = "GetStudentRegisteredCourses")]
        public async Task<ActionResult<List<StudentCoursesDto>>> GetStudentRegisteredCourses(StudentCoursesRequestDto studentCoursesRequest)
        {
            List<StudentCoursesDto> studentCourses=new();
            try
            {
                studentCourses = await _studentService.GetRegisteredCoursesAsync(studentCoursesRequest);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                var ext = ex;
            }

            if (studentCourses == null) return BadRequest();

            return Ok(studentCourses);
        }


        /// <summary>
        /// Gets all University courses
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Student")]
        [HttpGet(Name = "GetAllAvailableCourses")]
        public async Task<ActionResult<List<StudentCoursesDto>>> GetAllAvailableCourses()
        {
            List<StudentCoursesDto> universityCourses;
            try
            {
                universityCourses = await _courseService.GetAllCoursesAsync();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }


            if (universityCourses == null) return BadRequest();

            return Ok(universityCourses);
        }


        /// <summary>
        /// Un-registers from a course/course
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="studentCourses"></param>
        /// <returns></returns>
        [Authorize(Roles = "Student")]
        [HttpPost(Name = "UnregisterFromACourse")]
        public async Task<ActionResult<bool>> UnregisterFromACourse(CourseRegistrationRequestDto courseRegistration)
        {
            bool response;
            try
            {
                response = await _studentService.UnregisterAsync(courseRegistration);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(response);
        }


        /// <summary>
        /// Gets all university students
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet(Name = "GetAllUniversityStudents")]
        public async Task<ActionResult<List<StudentsResponseDto>>> GetAllUniversityStudents()
        {
            List<StudentsResponseDto> response;
            try
            {
                response = await _studentService.GetAllUniversityStudentsAsync();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(response);
        }
    }
}
