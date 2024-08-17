using afi.university.application.Common.Exceptions;
using afi.university.application.Services.Interfaces;
using afi.university.domain.Common.Enums;
using afi.university.domain.Entities;
using afi.university.domain.Repositories.Base;
using afi.university.shared.DataTransferObjects.Requests;
using afi.university.shared.DataTransferObjects.Responses;
using AutoMapper;

namespace afi.university.application.Services.Implementation
{
    internal class StudentService : IStudentService
    {     
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _repository;
        private readonly Random random = new Random();

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._mapper = mapper;
            this._repository = unitOfWork;            
        }

        /// <summary>
        /// Get all registered students
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<IEnumerable<StudentResponse>> GetAllStudentsAsync()
        {
            var students = await _repository.Users.GetByConditionAsync(e => e.Role.Equals(UserRole.Student), false);      
            if(students == null)
                throw new NotFoundException("Students not found.");

            return _mapper.Map<ICollection<StudentResponse>>(students);
        }

        /// <summary>
        /// Get list of all course registered for
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public async Task<StudentResponse> GetStudentRegisteredCoursesAsync(StudentRequest studentRequest)
        {
            // get student
            var student = await _repository.Students.GetByIdAsync(studentRequest.StudentId, false);
            if (student == null)
                throw new StudentNotFoundException(studentRequest.StudentId);
            
            var response = _mapper.Map<StudentResponse>(student);

            // get student courses
            var studentCourses = await _repository.StudentCourses.GetCoursesByStudentIdAsync(student.Id, false);

            foreach (var studentCourse in studentCourses)
            {
                // get course details
                var registeredCourse = _mapper.Map<CourseResponse>(await _repository.Courses.GetByIdAsync(studentCourse.CourseId, false));
                if (registeredCourse != null)
                {
                    registeredCourse.Registered = true;
                    response.Courses!.Add(registeredCourse);
                }
                
            }

            return response;
        }

        /// <summary>
        /// Registers a student to University
        /// </summary>
        /// <param name="registrationRequest"></param>
        /// <returns></returns>
        public async Task<RegistrationResponse> RegisterStudentAsync(RegistrationRequest registrationRequest)
        {
            // get student
            var student = await _repository.Users.GetUserByEmailAsync(registrationRequest.Email!, false);            
            if (student != null)            
                throw new StudentAlreadyExistsException(registrationRequest.Email!);            
                
            Student newStudent = _mapper.Map<Student>(registrationRequest);
            newStudent.StudentNumber = GenerateStudentNumber(newStudent.FirstName!, newStudent.LastName!);
            newStudent.DateCreated = DateTime.Now;
            newStudent.DateModified = DateTime.Now;
            newStudent.Role = UserRole.Student;
            
            await _repository.Users.CreateAsync(newStudent);
            _repository.SaveChangesAsync();

            var response = _mapper.Map<RegistrationResponse>(newStudent);             
            return response;
        }

        /// <summary>
        /// Registers a student to a Course
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="courseRegistrationRequest"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> EnrollCourseAsync(CourseRegistrationRequest courseRegistrationRequest)
        {
            // get student
            var student = await _repository.Users.GetByIdAsync(courseRegistrationRequest.StudentId, false);            
            if (student == null)
                throw new StudentNotFoundException(courseRegistrationRequest.StudentId);

            // get course
            var course = await _repository.Courses.GetByIdAsync(courseRegistrationRequest.CourseId, false);
            if (course == null)
                throw new CourseNotFoundException(courseRegistrationRequest.CourseId);

            // check if not already registered
            var check = await _repository.StudentCourses.GetByConditionAsync(e => e.StudentId.Equals(student.Id) && e.CourseId.Equals(course.Id), false);
            var alreadyRegistered = check.FirstOrDefault();
            if (alreadyRegistered != null)
                throw new StudentAlreadyRegisteredForCourseException(student.Id, course.Id);

            StudentCourse studentCourse = _mapper.Map<StudentCourse>(courseRegistrationRequest);
            studentCourse.DateCreated = DateTime.Now;
            studentCourse.DateModified = DateTime.Now;

            await _repository.StudentCourses.CreateAsync(studentCourse);
            _repository.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Un-registers student from course(s)
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="courseIds"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public async Task<bool> UnregisterAsync(CourseRegistrationRequest courseRegistrationRequest)
        {
            // get student
            var student = await _repository.Users.GetByIdAsync(courseRegistrationRequest.StudentId, false);
            if (student == null)
                throw new StudentNotFoundException(courseRegistrationRequest.StudentId);

            // get course
            var course = await _repository.Courses.GetByIdAsync(courseRegistrationRequest.CourseId, false);
            if (course == null)
                throw new CourseNotFoundException(courseRegistrationRequest.CourseId);

            await _repository.StudentCourses.DeleteAsync(new StudentCourse()
            {
                StudentId = student.Id,
                CourseId = course.Id,
            });

            _repository.SaveChangesAsync();

            return true;
        }


        #region Private Implementations
        /// <summary>
        /// Random string numbers
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <returns></returns>
        private string GenerateStudentNumber(string firstname, string lastname)
        {
            return GetRandomString(6, firstname, lastname) + DateTime.Now.Year.ToString();            
        }
        
        private string GetRandomString(int length, string firstname, string lastname)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"+ firstname + lastname;
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #endregion
    }
}
