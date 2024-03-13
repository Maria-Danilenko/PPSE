using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Models;
using WebApplication4.Services;

namespace WebApplication4.Pages
{
    public class OrderIndex : PageModel
    {
        private readonly IOrderService _orderService;

        public OrderIndex(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IEnumerable<Order> Orders { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Orders = await _orderService.GetAllOrdersAsync();
            return Page();
        }
    }
}