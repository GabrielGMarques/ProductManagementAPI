using AutoMapper;
using ProductManagement.Domain.Contracts.Repository;
using ProductManagement.Domain.Contracts.Services;
using ProductManagement.Domain.Entities;
using System.Text;
using System.Security.Cryptography;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductManagement.Domain.Dtos.Auth;
using ProductManagement.Domain.Dtos.Responses;
using ProductManagement.Domain.Dtos;

namespace ProductManagement.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _repository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<UserDto?>> GetAsync(int id)
        {
            var user = await _repository.GetAsync(id);
            var userDto = _mapper.Map<UserDto>(user);

            return new ServiceResponse<UserDto?>(true, userDto, "User retrieved successfully");
        }


        public async Task<ServiceResponse<string?>> Login(LoginDto userDto)
        {
            var response = new ServiceResponse<string?>();


            var user = await _repository.GetUserByName(userDto.Username);

            if (user == null || !VerifyPasswordHash(userDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                response.Message = "Invalid username or password";
                return response;
            }

            var jwtToken = GenerateJwtToken(user);

            response.Success = true;
            response.Data = jwtToken;
            response.Message = "User authenticated successfully";

            return response;
        }


        public async Task<ServiceResponse<int?>> Register(RegisterDto userDto)
        {
            var response = new ServiceResponse<int?>();

            if (await _repository.UserExists(userDto.Username))
            {
                response.Message = "Username already exists";
                return response;
            }

            var user = _mapper.Map<User>(userDto);
            CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            var resultId = await _repository.CreateAsync(user);

            response.Data = resultId;
            response.Success = true;

            return response;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["JwtSettings:ExpirationHours"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
