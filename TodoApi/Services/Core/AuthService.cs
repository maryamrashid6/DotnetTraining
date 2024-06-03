using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoApi.Entities;
using TodoApi.Services.Contracts;
using TodoApi.Services.Dtos;

namespace TodoApi.Services.Core
{
    public class AuthService : IAuthService
    {
        private readonly ToDoContext _dbContext;
        private readonly IConfiguration _configuration;

        //constructor
        public AuthService(ToDoContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public User Register(UserDto.UserAddRequestDto user)
        {
            // Logic to register a user
            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            return newUser;
        }

        public User Authenticate(string email, string password)
        {
            // Logic to authenticate a user
            //var user = _dbContext.Users.SingleOrDefault(x => x.Email == email && x.Password == password);

            // logic to authenticate a user with hashed password
            var user = _dbContext.Users.SingleOrDefault(x => x.Email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                return null;


            // return null if user not found
            if (user == null)
                return null;

            return user;
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }   
    }
}
