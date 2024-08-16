using afi.university.application.Models.Requests;
using afi.university.application.Models.Responses;
using afi.university.application.Services.Interfaces;
using afi.university.domain.Entities;
using afi.university.domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace afi.university.application.Services.Implementation
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            this._courseRepository = courseRepository;
        }
        public async Task<int> AddCourseAsync(CourseRequestDto courseRequest)
        {
            var response = await _courseRepository.CreateAsync(new Course() { Name= courseRequest.Name, Duration= courseRequest.Duration});
            if (response == 0)
                throw new ApplicationException($"Failed to create course ({courseRequest.Name})");

            return response;
        }

        public async Task<List<StudentCoursesDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllAsync(false);

            
            List<StudentCoursesDto> response = new();
            foreach (var course in courses)
            {
                //TODO: Automapper
                var cr = new StudentCoursesDto();
                {
                    cr.Id = course.Id;
                    cr.Name = course.Name;
                    cr.Duration = course.Duration;
                };

                response.Add(cr);
            }

            return response;
        }
    }
}
