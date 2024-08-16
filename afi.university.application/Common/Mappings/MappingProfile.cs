using afi.university.domain.Entities.Base;
using afi.university.shared.DataTransferObjects.Responses;
using AutoMapper;

namespace afi.university.application.Common.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginResponse, User>();
            CreateMap<User, LoginResponse>();
        }        
    }
}
