using afi.university.application.Models.Requests;
using afi.university.application.Models.Responses;
using afi.university.application.Services.Interfaces;
using afi.university.domain.Common.Enums;
using afi.university.domain.Entities;
using afi.university.domain.Entities.Base;
using afi.university.domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace afi.university.application.Services.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        
        public StudentService(IStudentRepository studentRepository, IUserRepository userRepository)
        {
            this._studentRepository = studentRepository;
            this._userRepository = userRepository;
        }

        /// <summary>
        /// Get list of all course registered for
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public async Task<List<StudentCoursesDto>> GetRegisteredCoursesAsync(int studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId) ?? throw new ApplicationException($"Failed to retrieve student ({studentId})");
            
            if (student.Courses == null)
                return new List<StudentCoursesDto>(); //Not registered courses yet

            List<StudentCoursesDto> studentCourses = new();
            foreach (var course in student.Courses)
            {
                studentCourses.Add(new StudentCoursesDto() { Id=course.Id, Name = course.Name, Duration = course.Duration, Registered=true });
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
            var response = await CreateAccountAsync(user);

            return new StudentRegistrationResponseDto() { StudentNumber = response.StudentNumber };
        }

        /// <summary>
        /// Registers a student to a Course
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="courseRegistrationRequest"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<StudentCoursesDto>> RegisterToACourseAsync(int studentId, List<StudentCoursesDto> courseRegistrationRequest)
        {
            var student = await _studentRepository.GetByIdAsync(studentId) ?? throw new ApplicationException($"Failed to retrieve student ({studentId})");
            
            student.Courses ??= new();
            foreach (var course in courseRegistrationRequest)
            {
                student.Courses.Add(new Course() { Name=course.Name, Duration=course.Duration});
                course.Registered = true;
            }
            
            var response = await _studentRepository.UpdateAsync(student);
            if(response > 0)
                throw new ApplicationException($"Failed to register user ({student.Email}) to course");

            // refresh registered courses
            return await this.GetRegisteredCoursesAsync(studentId);
        }

        /// <summary>
        /// Un-registers student from course(s)
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="courseIds"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public async Task<List<StudentCoursesDto>> UnregisterAsync(int studentId, List<StudentCoursesDto> courseRegistrationRequest)
        {
            var student = await _studentRepository.GetByIdAsync(studentId) ?? throw new ApplicationException($"Student ({studentId}) not registered for any course.");
            
            if (student.Courses == null)
                throw new ApplicationException($"Student ({studentId}) not registered for any course.");

            foreach (var course in courseRegistrationRequest)
            {
                var deleteCourse = student.Courses.SingleOrDefault(c => c.Id == course.Id);
                if(deleteCourse != null)
                    student.Courses.Remove(deleteCourse);
            }

            var response = await _studentRepository.UpdateAsync(student);
            if(response > 0)
                throw new ApplicationException($"Failed to un-register student ({student.Email}) to course");

            // refresh registered courses
            return await this.GetRegisteredCoursesAsync(studentId);
        }


        /// <summary>
        /// Gets all registered students
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public async Task<List<StudentsResponseDto>> GetAllUniversityStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync() ?? throw new ApplicationException($"Failed to retrieve students");

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
        /// Creates account to be used for login in
        /// </summary>
        /// <param name="registrationRequest"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        private async Task<User> CreateAccountAsync(StudentRegistrationRequestDto registrationRequest)
        {
            // TODO: Use Automapper
            User user = new()
            {
                FirstName = registrationRequest.FirstName,
                LastName = registrationRequest.LastName,
                Email = registrationRequest.Email,
                Password = registrationRequest.Password, //TODO: Encrypt password
                Role = UserRole.Student
            };

            int userreponse = await _userRepository.CreateAsync(user);
            if (userreponse > 0)
                throw new ApplicationException($"Faile to create user ({user.Email})");

            return user;
        }

        /// <summary>
        /// Creates student account
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        private async Task<Student> CreateAccountAsync(User user)
        {
            if(user == null)
                throw new ApplicationException($"Faile to create user");

            Student student = new()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password, //TODO: Encrypt password
                Role = UserRole.Student,
                StudentNumber = GenerateStudentNumber(user.FirstName, user.LastName),
                Courses = new List<Course>()
            };

            var response = await _studentRepository.CreateAsync(student);
            if (response > 0)
                throw new ApplicationException($"Faile to create student ({user.Email})");

            return student;
        }

        private string GenerateStudentNumber(string firstname, string lastname)
        {            
            return $"{firstname.Substring(0, 3)} {lastname.Substring(lastname.Length - 4, 3)}";
        }


        #endregion
    }
}
