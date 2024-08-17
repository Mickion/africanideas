using afi.university.domain.Entities;
using afi.university.domain.Repositories.Base;
using afi.university.application.Models.Requests;
using afi.university.application.Models.Responses;
using afi.university.application.Services.Interfaces;
using afi.university.shared.DataTransferObjects.Requests;
using afi.university.application.Common.Exceptions;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using afi.university.shared.DataTransferObjects.Responses;

namespace afi.university.application.Services.Implementation
{
    internal class CourseService : ICourseService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _repository;
        

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper)
        {            
            this._repository = unitOfWork;
            this._mapper = mapper;
        }

        /// <summary>
        /// Creates new course
        /// </summary>
        /// <param name="createCourseRequest"></param>
        /// <returns></returns>
        /// <exception cref="CourseAlreadyExistsException"></exception>
        public async Task<bool> AddCourseAsync(CreateCourseRequest createCourseRequest)
        {
            var course = await _repository.Courses.GetByConditionAsync(c => c.Name!.Equals(createCourseRequest.Name), false);
            var courseFound = course.SingleOrDefault();
            if(courseFound != null)
                throw new CourseAlreadyExistsException(createCourseRequest.Name!);            
            
            var newCourse = _mapper.Map<Course>(createCourseRequest);
            newCourse.DateCreated = DateTime.Now;
            newCourse.DateModified = DateTime.Now;

            await _repository.Courses.CreateAsync(newCourse);
            _repository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<StudentCoursesResponse>> GetAllCoursesAsync()
        {
            var courses = await _repository.Courses.GetAllAsync(false);
            if (courses == null)
                throw new NotFoundException("There are not courses found.");

            return _mapper.Map<ICollection<StudentCoursesResponse>>(courses);

        }
    }
}