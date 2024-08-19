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

        /// <summary>
        /// Gets all courses
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<IEnumerable<CourseResponse>> GetAllCoursesAsync()
        {
            var courses = await _repository.Courses.GetAllAsync(false);

            return courses == null
                ? throw new NotFoundException("There are not courses found.")
                : (IEnumerable<CourseResponse>)_mapper.Map<ICollection<CourseResponse>>(courses);
        }

        /// <summary>
        /// Gets course and registered students
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        /// <exception cref="CourseNotFoundException"></exception>
        public async Task<CourseStudentsResponse> GetCourseStudentsAsync(Guid courseId)
        {
            // get studentcourse link table
            var cr = await _repository.StudentCourses.GetByConditionAsync(c => c.CourseId.Equals(courseId), trackChanges: false);
            var courses = cr.ToList();

            if (!courses.Any())
                throw new CourseNotFoundException(courseId);

            // get course details
            var courseDetails = await _repository.Courses.GetByIdAsync(courseId, trackChanges: false);
            var courseStudents = new CourseStudentsResponse
            {
                Id = courseDetails.Id,
                Name = courseDetails.Name,
                NQFLevel = courseDetails.NQFLevel,
                Duration = courseDetails.Duration,
                Students = new List<StudentResponse>()
            };

            foreach (var course in courses)
            {
                // get student details
                var stu = await _repository.Students.GetByIdAsync(course.StudentId, trackChanges: false);
                if(stu == null) continue;
                
                courseStudents.Students.Add(_mapper.Map<StudentResponse>(stu));

            }

            return courseStudents;
        }
    }
}