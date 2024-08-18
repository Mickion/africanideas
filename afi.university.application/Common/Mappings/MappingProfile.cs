using afi.university.domain.Entities;
using afi.university.domain.Entities.Base;
using afi.university.shared.DataTransferObjects.Requests;
using afi.university.shared.DataTransferObjects.Responses;
using AutoMapper;

namespace afi.university.application.Common.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {            
            CreateMap<User, LoginResponse>();

            CreateMap<Course, CourseResponse>();
            CreateMap<ICollection<Course>, ICollection<CourseResponse>>();

            CreateMap<Student, StudentResponse>();
            CreateMap<ICollection<Student>, ICollection<StudentResponse>>();

            CreateMap<RegistrationRequest, Student>();
            CreateMap<Student, RegistrationResponse>();

            CreateMap<CourseRegistrationRequest, StudentCourse>();

            CreateMap<CreateCourseRequest, Course>();

            CreateMap<Course, CourseResponse>();
            CreateMap<ICollection<Course>, ICollection<CourseResponse>>();
        }        
    }
}
