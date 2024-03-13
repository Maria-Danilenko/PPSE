using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication4.Services;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserAuthController : ControllerBase
    {
        private readonly IUserAuthService _userAuthService;

        public UserAuthController(IUserAuthService userAuthService)
        {
            _userAuthService = userAuthService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(string email, string password)
        {
            var token = await _userAuthService.Authenticate(email, password);

            if (token == null)
            {
                return BadRequest(new { message = "Invalid email or password" });
            }

            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationRequest request)
        {
            var result = await _userAuthService.Register(request);

            if (result != true)
            {
                return BadRequest(result = false);
            }

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserAuth>> GetById(int id)
        {
            var user = await _userAuthService.GetUserAuthByIdAsync(id);

            if (user is NullUserAuth)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
