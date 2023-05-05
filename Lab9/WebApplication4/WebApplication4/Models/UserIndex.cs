using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Models;
using WebApplication4.Services;

namespace WebApplication4.Pages
{
    public class UserIndex : PageModel
    {
        private readonly IUserService _userService;

        public UserIndex(IUserService userService)
        {
            _userService = userService;
        }

        public IEnumerable<User> Users { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Users = await _userService.GetAllUsersAsync();
            return Page();
        }
    }
}