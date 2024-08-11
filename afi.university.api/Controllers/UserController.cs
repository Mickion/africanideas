using afi.university.application.Common.Exceptions;
using afi.university.application.Models.Requests;
using afi.university.application.Models.Responses;
using afi.university.application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace afi.university.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        /// <summary>
        /// Login to application
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost(Name = "Login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto loginRequest)
        {
            LoginResponseDto response;
            try
            {
                response = await _userService.LoginAsync(loginRequest);
            }
            catch (NotFoundException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (domain.Common.Exceptions.NotFoundException)
            {
                return Unauthorized("Invalid login details");
            }

            if(response == null) return Unauthorized();
            
            if (string.IsNullOrWhiteSpace(response.Token)) return Unauthorized();

            return Ok(response);
        }
    }
}
