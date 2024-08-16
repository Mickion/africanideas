using afi.university.application.Services.Interfaces;
using afi.university.domain.Common.Enums;
using afi.university.domain.Entities.Base;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using afi.university.application.Common.Exceptions;
using afi.university.shared.DataTransferObjects.Responses;
using afi.university.shared.DataTransferObjects.Requests;
using afi.university.domain.Repositories.Base;
using AutoMapper;

namespace afi.university.application.Services.Implementation
{
    internal sealed class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _repository;
        private readonly IConfiguration _configuration;
        
        public UserService(IUnitOfWork repositories, IConfiguration configuration, IMapper mapper)
        {
            this._mapper = mapper;
            this._repository = repositories;
            this._configuration = configuration;            
        }

        /// <summary>
        /// Authenticates user and returns JWT token
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns>LoginResponse</returns>
        /// <exception cref="InvalidCredentialsException"></exception>
        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            if (string.IsNullOrWhiteSpace(loginRequest.Email)
                || string.IsNullOrWhiteSpace(loginRequest.Password))
                throw new InvalidCredentialsException("Invalid Username or Password.");

            // dont track changes for performance improvements
            User user = await _repository.Users.GetUserLoginsAsync(loginRequest.Email, loginRequest.Password, false);

            if (user == null)
                throw new InvalidCredentialsException("Invalid Username or Password.");

            LoginResponse loginResponse = _mapper.Map<LoginResponse>(user);
            loginResponse.Token = GenerateToken(user);

            return loginResponse;
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
