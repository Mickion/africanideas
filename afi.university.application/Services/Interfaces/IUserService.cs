using afi.university.application.Models.Requests;
using afi.university.application.Models.Responses;
using afi.university.domain.Common.Enums;

namespace afi.university.application.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequest);
    }
}
