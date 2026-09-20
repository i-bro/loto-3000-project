using LotoApp.DataAccess.Interfaces;
using LotoApp.Domain.Enums;
using LotoApp.Domain.Models;
using LotoApp.DTOs;
using LotoApp.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace LotoApp.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> LoginUserAsync(LoginUserDto loginUserDto)
        {
            if(loginUserDto == null)
            {
                throw new ArgumentNullException("Model cannot be null");
            }
            if(string.IsNullOrEmpty(loginUserDto.Username) || string.IsNullOrEmpty(loginUserDto.Password))
            {
                throw new ArgumentNullException("Username and password are rewuired fields");
            }

            var hashedPassword = GenerateHash(loginUserDto.Password);

            var userDb = await _userRepository.GetByUsernameAsync(loginUserDto.Username);

            if(userDb == null || userDb.Password != hashedPassword)
            {
                throw new UnauthorizedAccessException("Wrong username or password");
            }

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes("Our secret secret secret secret secret secret secret secret key");
            var role = userDb.Username == "petko" ? Role.Admin : Role.Player;

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddHours(2),
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, loginUserDto.Username),
                    new Claim(ClaimTypes.Role, role.ToString()),
                    new Claim("id", userDb.Id.ToString()),
                    new Claim("userFullname", $"{userDb.FirstName} {userDb.LastName}")
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            string tokenString = jwtSecurityTokenHandler.WriteToken(token);

            return tokenString;


        }

        public async Task RegisterUserAsync(RegisterUserDto registerUserDto)
        {
           if(registerUserDto == null)
            {
                throw new ArgumentNullException("Modle canot be empty");
            }
           if(string.IsNullOrEmpty(registerUserDto.Firstname) || string.IsNullOrEmpty(registerUserDto.Lastname))
            {
                throw new ArgumentNullException("Firstname and lastname are rewuired fields ");
            }
            if (string.IsNullOrEmpty(registerUserDto.Username))
            {
                throw new ArgumentNullException("Username is a rewuired field");
            }
            if (string.IsNullOrEmpty(registerUserDto.Password))
            {
                throw new ArgumentNullException("Password is a rewuired field");
            }

            var existingUser = await _userRepository.GetByUsernameAsync(registerUserDto.Username);
            if(existingUser != null)
            {
                throw new ArgumentException($"User with username {registerUserDto.Username} already exists");
            }

            string strongPasswordRegex = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{5,}$";
            if (!Regex.IsMatch(registerUserDto.Password, strongPasswordRegex))
            {
                throw new ArgumentException("Password is not a strong password");
            }

            User user = new User
            {
                FirstName = registerUserDto.Firstname,
                LastName = registerUserDto.Lastname,
                Username = registerUserDto.Username,
                Password = GenerateHash(registerUserDto.Password)
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        private string GenerateHash(string password)
        {
            using var md5Has = MD5.Create();
            var passwordBytes = Encoding.ASCII.GetBytes(password);
            var hashedBytes = md5Has.ComputeHash(passwordBytes);

            
            return Convert.ToHexString(hashedBytes);
        }
    }
}
