using afi.university.application.Common.Exceptions;
using afi.university.application.Models.Requests;
using afi.university.application.Models.Responses;
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

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._mapper = mapper;
            this._repository = unitOfWork;            
        }

        public async Task<IEnumerable<StudentResponse>> GetAllStudentsAsync()
        {
            var students = await _repository.Users.GetByConditionAsync(e => e.Role.Equals(UserRole.Student), false);      
            if(students == null)
                throw new NotFoundException("Students not found");

            return _mapper.Map<ICollection<StudentResponse>>(students);
        }

        /// <summary>
        /// Get list of all course registered for
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public async Task<StudentResponse> GetStudentCoursesAsync(StudentRequest studentRequest)
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
                response.Courses!.Add(_mapper.Map<CourseResponse>(await _repository.Courses.GetByIdAsync(studentCourse.CourseId, false)));
            }

            return response;
        }

        /// <summary>
        /// Registers a student to University
        /// </summary>
        /// <param name="registrationRequest"></param>
        /// <returns></returns>
        public async Task<StudentRegistrationResponseDto> RegisterToUniversityAsync(StudentRegistrationRequestDto registrationRequest)
        {
            throw new NotImplementedException();
            //var user = await CreateAccountAsync(registrationRequest);

            //return new StudentRegistrationResponseDto() { StudentNumber = GenerateStudentNumber(user.FirstName!, user.LastName!) };
        }

        /// <summary>
        /// Registers a student to a Course
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="courseRegistrationRequest"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> RegisterCourseAsync(CourseRegistrationRequestDto courseRegistration)
        {
            throw new NotSupportedException();
            //var student = await _studentRepository.GetByIdAsync(courseRegistration.StudentId, false);
            //if (student == null) return false;

            //var course = await _courseRepository.GetCourseByNameAsync(courseRegistration.Name!, false);
            //if (course == null) return false;

            //StudentCourse studentCourse = new()
            //{
            //    StudentId = student.Id,
            //    CourseId = course.Id,
            //    Student = student,
            //    Course = course
            //};

            //var response = await _studentCourseRepository.CreateAsync(studentCourse);

            //if(!response)
            //    throw new ApplicationException($"Failed to register user ({student.Email}) to course {course.Name}");
            
            //return true;
        }

        /// <summary>
        /// Un-registers student from course(s)
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="courseIds"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public async Task<bool> UnregisterAsync(CourseRegistrationRequestDto courseRegistration)
        {
            throw new NotImplementedException ();
            //var courses = await GetRegisteredCoursesAsync(new StudentCoursesRequestDto() { StudentId = courseRegistration.StudentId });            
            //if (courses == null) return false;

            //var course = courses.FirstOrDefault(x => x.Name == courseRegistration.Name);
            //if (course == null) return false;

            //StudentCourse studentCourse = new();
            //studentCourse.Course!.Id = course.Id;
            //studentCourse.Course.Name = course.Name;
            //studentCourse.Course.Duration = course.Duration;

            ////TODO: Fix this, it will not work
            //var response = await _studentCourseRepository.DeleteAsync(course.Id);
            //if (!response)
            //    throw new ApplicationException($"Failed to un-register student ({courseRegistration.StudentId}) from course {courseRegistration.Name}");

            //// refresh registered courses
            //return true;
        }


        /// <summary>
        /// Gets all registered students
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public async Task<List<StudentsResponseDto>> GetAllUniversityStudentsAsync()
        {
            throw new NotImplementedException();
            //var students = await _studentRepository.GetAllAsync(false);

            //List<StudentsResponseDto> response=new();
            //foreach (var student in students)
            //{
            //    response.Add(
            //        new StudentsResponseDto
            //        {
            //            Id = student.Id,
            //            StudentNumber = student.StudentNumber,
            //            FirstName = student.FirstName,
            //            LastName = student.LastName,                        
            //            Email = student.Email
            //        }
            //    );
            //}

            //return response;
        }


        #region Private Implementations
        /// <summary>
        /// Creates student account
        /// </summary>
        /// <param name="registrationRequest"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        private async Task<Student> CreateAccountAsync(StudentRegistrationRequestDto registrationRequest)
        {
            throw new NotImplementedException();
            // TODO: Use Automapper  
            //Student student = new()
            //{                
            //    FirstName = registrationRequest.FirstName,
            //    LastName = registrationRequest.LastName,
            //    Email = registrationRequest.Email,
            //    Password = registrationRequest.Password, //TODO: Encrypt password
            //    Role = UserRole.Student,
            //    StudentNumber = GenerateStudentNumber(registrationRequest.FirstName, registrationRequest.LastName)
            //};

            //var response = await _studentRepository.CreateAsync(student);
            //if (!response)
            //    throw new ApplicationException($"Failed to create student ({registrationRequest.Email})");

            //return student;
        }

        private string GenerateStudentNumber(string firstname, string lastname)
        {            
            return $"{firstname.Substring(0, 3)} {lastname.Substring(lastname.Length - 3, 3)}";
        }




        #endregion
    }
}
