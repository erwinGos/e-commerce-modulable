using AutoMapper;
using Data.DTO.User;
using Data.Managers;
using Data.Repository.Contract;
using Data.Services.Contract;
using Database.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Configuration;


namespace Data.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        private readonly IConfiguration _configuration;

        private readonly IMapper _mapper;

        public AuthenticationService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("UserId", Convert.ToString(user.Id)),
                new Claim("UserEmail", user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:ExpiresInHours"])),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User> SignUp(SignUpUser signUpUser)
        {
            try
            {
                signUpUser.Password = IdentityManager.HashPassword(signUpUser.Password);
                var aftermapper = _mapper.Map<User>(signUpUser);
                return await _userRepository.Insert(aftermapper);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
