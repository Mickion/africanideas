using afi.university.application.Models.Requests;
using afi.university.application.Models.Responses;
using afi.university.application.Services.Interfaces;
using afi.university.domain.Common.Enums;
using afi.university.domain.Entities;
using afi.university.domain.Repositories;

namespace afi.university.application.Services.Implementation
{
    public class StudentService : IStudentService
    {        
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentCourseRepository _studentCourseRepository;
        private readonly IStudentRepository _studentRepository;
        
        public StudentService(
            ICourseRepository courseRepository,
            IStudentRepository studentRepository,                         
            IStudentCourseRepository studentCourseRepository)
        {
            this._studentRepository = studentRepository;            
            this._courseRepository = courseRepository;
            this._studentCourseRepository = studentCourseRepository;
        }

        /// <summary>
        /// Get list of all course registered for
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public async Task<List<StudentCoursesDto>> GetRegisteredCoursesAsync(StudentCoursesRequestDto studentCoursesRequest)
        {
            List<StudentCoursesDto> studentCourses = new();
            
            var courses = await _studentCourseRepository.GetCoursesByStudentIdAsync(studentCoursesRequest.StudentId);

            foreach (var course in courses)
            {
                var cr = await _courseRepository.GetByIdAsync(course.CourseId);
                studentCourses.Add(
                    new StudentCoursesDto()
                    {
                        Id = cr.Id,
                        Name = cr.Name,
                        Duration = cr.Duration,
                        Registered = true
                    }
                );
            }

            return studentCourses;
        }

        /// <summary>
        /// Registers a student to University
        /// </summary>
        /// <param name="registrationRequest"></param>
        /// <returns></returns>
        public async Task<StudentRegistrationResponseDto> RegisterToUniversityAsync(StudentRegistrationRequestDto registrationRequest)
        {
            var user = await CreateAccountAsync(registrationRequest);

            return new StudentRegistrationResponseDto() { StudentNumber = GenerateStudentNumber(user.FirstName!, user.LastName!) };
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
            var student = await _studentRepository.GetByIdAsync(courseRegistration.StudentId);
            if (student == null) return false;

            var course = await _courseRepository.GetCourseByNameAsync(courseRegistration.Name!);
            if (course == null) return false;

            StudentCourse studentCourse = new()
            {
                StudentId = student.Id,
                CourseId = course.Id,
                Student = student,
                Course = course
            };

            var response = await _studentCourseRepository.CreateAsync(studentCourse);

            if(response == 0)
                throw new ApplicationException($"Failed to register user ({student.Email}) to course {course.Name}");
            
            return true;
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
            var courses = await GetRegisteredCoursesAsync(new StudentCoursesRequestDto() { StudentId = courseRegistration.StudentId });            
            if (courses == null) return false;

            var course = courses.FirstOrDefault(x => x.Name == courseRegistration.Name);
            if (course == null) return false;

            StudentCourse studentCourse = new();
            studentCourse.Course!.Id = course.Id;
            studentCourse.Course.Name = course.Name;
            studentCourse.Course.Duration = course.Duration;

            var response = await _studentCourseRepository.DeleteAsync(studentCourse);
            if (response == 0)
                throw new ApplicationException($"Failed to un-register student ({courseRegistration.StudentId}) from course {courseRegistration.Name}");

            // refresh registered courses
            return (response != 0);
        }


        /// <summary>
        /// Gets all registered students
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public async Task<List<StudentsResponseDto>> GetAllUniversityStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();

            List<StudentsResponseDto> response=new();
            foreach (var student in students)
            {
                response.Add(
                    new StudentsResponseDto
                    {
                        Id = student.Id,
                        StudentNumber = student.StudentNumber,
                        FirstName = student.FirstName,
                        LastName = student.LastName,                        
                        Email = student.Email
                    }
                );
            }

            return response;
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
            // TODO: Use Automapper  
            Student student = new()
            {                
                FirstName = registrationRequest.FirstName,
                LastName = registrationRequest.LastName,
                Email = registrationRequest.Email,
                Password = registrationRequest.Password, //TODO: Encrypt password
                Role = UserRole.Student,
                StudentNumber = GenerateStudentNumber(registrationRequest.FirstName, registrationRequest.LastName)
            };

            var response = await _studentRepository.CreateAsync(student);
            if (response == 0)
                throw new ApplicationException($"Failed to create student ({registrationRequest.Email})");

            return student;
        }

        private string GenerateStudentNumber(string firstname, string lastname)
        {            
            return $"{firstname.Substring(0, 3)} {lastname.Substring(lastname.Length - 3, 3)}";
        }


        #endregion
    }
}
