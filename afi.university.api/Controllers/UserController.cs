using afi.university.application.Common.Exceptions;
using afi.university.application.Models.Requests;
using afi.university.application.Models.Responses;
using afi.university.application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace afi.university.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this._userService = userService;
            this._logger = logger;
        }

        /// <summary>
        /// Login to application
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost(Name = "Login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto loginRequest)
        {
            _logger.LogDebug("Login requested by username {1}", loginRequest.Email);

            LoginResponseDto? response = null;
            try
            {
                response = await _userService.LoginAsync(loginRequest);
            }            
            catch (InvalidCredentialsException ex)
            {
                _logger.LogWarning("Failed login attempt {0} - ",  ex);
                return Unauthorized(ex.Message);
            }
            catch (domain.Common.Exceptions.NotFoundException ex)
            {
                _logger.LogWarning("Failed login attempt {0} - ", ex);
                return Unauthorized("Invalid Username or Password.");                
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed login attempt {0} - ", ex);
            }

            if(response == null) return Unauthorized("Invalid Username or Password.");
            
            if (string.IsNullOrWhiteSpace(response.Token)) return Unauthorized();

            return Ok(response);
        }
    }
}
