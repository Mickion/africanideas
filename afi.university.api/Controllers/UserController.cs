using afi.university.application.Common.Exceptions;
using afi.university.application.Models.Requests;
using afi.university.application.Models.Responses;
using afi.university.application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace afi.university.api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }
        [HttpGet(Name = "Login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto loginRequest)
        {
            LoginResponseDto results;
            try
            {
                results = await _userService.LoginAsync(loginRequest);
            }
            catch (NotFoundException)
            {
                return Unauthorized();
            }

            if(results == null) return Unauthorized();
            
            if (string.IsNullOrWhiteSpace(results.Token)) return Unauthorized();

            return Ok(results);
        }
    }
}
