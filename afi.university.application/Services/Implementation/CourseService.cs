using afi.university.domain.Entities;
using afi.university.domain.Repositories.Base;
using afi.university.application.Services.Interfaces;
using afi.university.shared.DataTransferObjects.Requests;
using afi.university.application.Common.Exceptions;
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
            if(await _repository.Courses.ExistsAsync(c => c.Name!.Equals(createCourseRequest.Name), trackChanges:false))
                throw new CourseAlreadyExistsException(createCourseRequest.Name!);            
            
            var newCourse = _mapper.Map<Course>(createCourseRequest);
            newCourse.DateCreated = DateTime.Now;
            newCourse.DateModified = DateTime.Now;

            await _repository.Courses.CreateAsync(newCourse);
            _repository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CourseResponse>> GetAllCoursesAsync()
        {
            var courses = await _repository.Courses.GetAllAsync(false);

            return courses == null
                ? throw new NotFoundException("There are not courses found.")
                : (IEnumerable<CourseResponse>)_mapper.Map<ICollection<CourseResponse>>(courses);
        }
    }
}