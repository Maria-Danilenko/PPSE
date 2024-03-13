using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication4.Models;

namespace WebApplication4.Services
{
    public interface ITokenService
    {
        string GenerateToken(UserAuth user);
    }

    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string GetConfigurationValue(string key)
        {
            return _configuration[key];
        }

        private byte[] GetKey()
        {
            var secretKey = GetConfigurationValue("my_secret_key_1234");
            return Encoding.ASCII.GetBytes(secretKey);
        }

        public string GenerateToken(UserAuth user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(CreateTokenDescriptor(user));
            return tokenHandler.WriteToken(token);
        }

        private SecurityTokenDescriptor CreateTokenDescriptor(UserAuth user, int expireDays = 7)
        {
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(expireDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(GetKey()), SecurityAlgorithms.HmacSha256Signature)
            };
        }

        
    }

}
