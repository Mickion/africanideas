using afi.university.application.Common.Exceptions;
using afi.university.application.Services.Interfaces;
using afi.university.shared.DataTransferObjects.Requests;
using afi.university.shared.DataTransferObjects.Responses;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest loginRequest)
        {            
            //TODO: Implement exception middleware & catch InvalidCredentialsException there
            LoginResponse response;
            try
            {
                response = await _userService.LoginAsync(loginRequest);
            }            
            catch (InvalidCredentialsException ex)
            {
                _logger.LogWarning("Failed login attempt {0} - ",  ex.Message);
                return Unauthorized(ex.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed login attempt {0} - ", ex);
                return BadRequest(ex.Message);
            }

            if (response == null) 
            {
                _logger.LogWarning("Failed login attempt - Response is null.");
                return Unauthorized("Invalid Username or Password.");
            }

            if (string.IsNullOrWhiteSpace(response.Token)) 
            {
                _logger.LogWarning("Failed login attempt - Token is null.");
                return Unauthorized("Invalid Username or Password.");
            }

            return Ok(response);
        }
    }

}
