using WebApplication4.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication4.Services
{
    public interface IUserAuthService
    {
        Task<string> Authenticate(string email, string password);
        Task<bool> Register(string firstName, string lastName, string email, string password);
        Task<UserAuth> GetUserAuthByIdAsync(int id);
    }

    public class UserAuthService : IUserAuthService
    {
        private readonly List<UserAuth> _users;
        private readonly IConfiguration _configuration;

        public UserAuthService(IConfiguration configuration)
        {
            _users = new List<UserAuth>
            {
                new UserAuth { Id = 1, FirstName = "Camilla", LastName = "Stuart", Email = "reanna.renner@trantow.net",
                        DateOfBirth = new DateTime(1985, 6, 10), Password = "password", LastLogin = DateTime.Now },
                new UserAuth { Id = 2, FirstName = "Philippa", LastName = "Cook", Email = "bfeil@hotmail.com",
                        DateOfBirth = new DateTime(1990, 3, 20), Password = "password1", LastLogin = DateTime.Now },
                new UserAuth { Id = 3, FirstName = "Arran", LastName = "Mcgee", Email = "lukas.von@schneider.com",
                        DateOfBirth = new DateTime(1989, 7, 28), Password = "password11", LastLogin = DateTime.Now },
                new UserAuth { Id = 4, FirstName = "Zaid", LastName = "Haines", Email = "gunnar17@leuschke.com",
                        DateOfBirth = new DateTime(2004, 11, 28), Password = "password111", LastLogin = DateTime.Now },
                new UserAuth { Id = 5, FirstName = "Aleksander", LastName = "Richard", Email = "carmela75@kuhlman.com",
                        DateOfBirth = new DateTime(1984, 6, 30), Password = "password1111", LastLogin = DateTime.Now }
            };
            _configuration = configuration;
        }

        public async Task<string> Authenticate(string email, string password)
        {
            var user = _users.FirstOrDefault(x => x.Email == email && x.Password == EncryptionService.EncryptPassword(password));

            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("my_secret_key_1234");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> Register(string firstName, string lastName, string email, string password)
        {
            if (_users.Any(x => x.Email == email))
            {
                return false;
            }

            var user = new UserAuth
            {
                Id = _users.Count + 1,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            _users.Add(user);

            return true;
        }

        public async Task<UserAuth> GetUserAuthByIdAsync(int id)
        {
            return await Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
        }
    }
}
