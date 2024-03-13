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
        Task<bool> Register(UserRegistrationRequest request);
        Task<UserAuth> GetUserAuthByIdAsync(int id);
    }

    public class UserAuthService : IUserAuthService
    {
        private readonly List<UserAuth> _users;
        private readonly ITokenService _tokenService;

        public UserAuthService(IConfiguration configuration, ITokenService tokenService)
        {
            _users = new List<UserAuth>
            {
                new UserAuth { Id = 1, FirstName = "Mariia", LastName = "Danylenko", Email = "masha.danilenko666@gmail.com",
                            DateOfBirth = new DateTime(2022, 11, 12), Password = "password123", LastLogin = DateTime.Now },
            };
            _tokenService = tokenService;
        }

        public async Task<string> Authenticate(string email, string password)
        {
            var user = ValidateUserCredentials(email, password);
            if (user == null)
            {
                return null;
            }
            return _tokenService.GenerateToken(user);
        }

        private UserAuth ValidateUserCredentials(string email, string password)
        {
            return _users.FirstOrDefault(x => x.Email == email && x.Password == EncryptionService.EncryptPassword(password));
        }
     
        public async Task<bool> Register(UserRegistrationRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Request cannot be null.");
            }

            if (_users.Any(x => x.Email == request.Email))
            {
                return false;
            }

            var user = new UserAuth
            {
                Id = _users.Count + 1,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password
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
