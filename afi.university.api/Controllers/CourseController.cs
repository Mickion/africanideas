using afi.university.application.Models.Requests;
using afi.university.application.Models.Responses;
using afi.university.application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace afi.university.api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            this._courseService = courseService;
        }

        /// <summary>
        /// Creates new course
        /// </summary>
        /// <param name="courseRequest"></param>
        /// <returns></returns>
        [HttpPost(Name = "AddCourse")]
        public async Task<ActionResult<int>> AddCourseAsync(CourseRequestDto courseRequest)
        {
            int response;
            try
            {
                response = await _courseService.AddCourseAsync(courseRequest);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(response);
        }

        /// <summary>
        /// Gets all University courses
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name ="GetAllUniversityCourses")]
        public async Task<ActionResult<List<StudentCoursesDto>>> GetAllUniversityCourses()
        {
            List<StudentCoursesDto> response;
            try
            {
                response = await _courseService.GetAllCoursesAsync();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(response);
        }

    }
}
