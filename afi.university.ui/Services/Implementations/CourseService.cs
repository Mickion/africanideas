﻿
using afi.university.ui.Services.Interfaces;
using afi.university.ui.Services.Interfaces.Authentication;
using afi.university.shared.DataTransferObjects.Responses;

namespace afi.university.ui.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly IHttpService _httpService;
        public CourseService(IHttpService httpService) => _httpService = httpService;       

        public async Task<IEnumerable<CourseResponse>> GetAllUniversityCoursesAsync()
        {
            var response =  await _httpService.Get<IEnumerable<CourseResponse>>("/courses");
            return response;
        }
    }
}
