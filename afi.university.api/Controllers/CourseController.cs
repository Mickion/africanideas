using afi.university.application.Common.Exceptions;
using afi.university.application.Services.Interfaces;
using afi.university.shared.DataTransferObjects.Requests;
using afi.university.shared.DataTransferObjects.Responses;
using Microsoft.AspNetCore.Mvc;

namespace afi.university.api.Controllers
{

    [Route("api/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ILogger<CourseController> _logger;

        public CourseController(ICourseService courseService, ILogger<CourseController> logger)
        {
            this._courseService = courseService;
            this._logger = logger;
        }

        /// <summary>
        /// Gets all University courses
        /// </summary>
        /// <returns></returns>
        
        [HttpGet]
        //[Authorize(Roles = "Admin,Student")]
        public async Task<ActionResult<IEnumerable<StudentCoursesResponse>>> GetAllUniversityCourses()
        {
            IEnumerable<StudentCoursesResponse> courses;
            try
            {
                courses = await _courseService.GetAllCoursesAsync();
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning("Failed getting list of students {0} - ", ex.Message);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed getting list of students {0} - ", ex);
                return BadRequest(ex.Message);
            }


            return (!courses.Any()) ? NotFound() : Ok(courses);
        }

        /// <summary>
        /// Creates new course
        /// </summary>
        /// <param name="courseRequest"></param>
        /// <returns></returns>

        [HttpPost]
        //[Authorize(Roles = "Admin, Lecture")]        
        public async Task<ActionResult<bool>> AddCourseAsync([FromBody] CreateCourseRequest createCourseRequest)
        {
            bool response;
            try
            {
                response = await _courseService.AddCourseAsync(createCourseRequest);
            }
            catch (CourseAlreadyExistsException ex)
            {
                _logger.LogWarning("Course creation failure {0} - ", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Course creation failure  {0} - ", ex);
                return BadRequest(ex.Message);
            }

            return Ok(response);
        }



    }
}
