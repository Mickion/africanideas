using afi.university.application.Models.Responses;
using afi.university.application.Services.Interfaces;
using afi.university.domain.Common.Enums;
using afi.university.domain.Entities.Base;
using afi.university.domain.Repositories;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using afi.university.application.Models.Requests;
using afi.university.application.Common.Exceptions;

namespace afi.university.application.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        
        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            this._userRepository = userRepository;
            this._configuration = configuration;
        }

        /// <summary>
        /// Authenticates user and returns JWT token
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        /// <exception cref="InvalidCredentialsException"></exception>
        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequest)
        {
            if (string.IsNullOrWhiteSpace(loginRequest.Email) 
                || string.IsNullOrWhiteSpace(loginRequest.Password))
                throw new InvalidCredentialsException("Invalid Username or Password.");

            User user = await _userRepository.GetUserLoginsAsync(loginRequest.Email, loginRequest.Password, false);

            if (user == null)
                throw new InvalidCredentialsException("Invalid Username or Password.");

            LoginResponseDto response = new()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                Token = this.GenerateToken(user)
            };

            return response;
        }

        #region Private Implementation methods
        /// <summary>
        /// Generates role based JWT Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email!),                
                new Claim(ClaimTypes.Role, Enum.GetName(typeof(UserRole), user.Role))
            };


            var token = new JwtSecurityToken(
                _configuration["JwtSettings:Issuer"],
                _configuration["JwtSettings:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion
    }
}
