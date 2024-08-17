using afi.university.domain.Entities;
using afi.university.domain.Repositories.Base;
using afi.university.application.Models.Requests;
using afi.university.application.Models.Responses;
using afi.university.application.Services.Interfaces;

namespace afi.university.application.Services.Implementation
{
    internal class CourseService : ICourseService
    {
        private readonly IUnitOfWork _repository;
        public CourseService(IUnitOfWork unitOfWork)
        {            
            this._repository = unitOfWork;
        }
        public async Task<bool> AddCourseAsync(CourseRequestDto courseRequest)
        {
            var response = await _repository.Courses.CreateAsync(new Course() { Name= courseRequest.Name, Duration= courseRequest.Duration});
            if (!response)
                throw new ApplicationException($"Failed to create course ({courseRequest.Name})");

            return response;
        }

        public async Task<List<StudentCoursesDto>> GetAllCoursesAsync()
        {
            var courses = await _repository.Courses.GetAllAsync(false);

            
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
