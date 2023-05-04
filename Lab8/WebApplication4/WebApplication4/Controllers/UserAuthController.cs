using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication4.Services;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<IActionResult> Register(string firstName, string lastName, string email, string password)
        {
            var result = await _userAuthService.Register(firstName, lastName, email, password);

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

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
