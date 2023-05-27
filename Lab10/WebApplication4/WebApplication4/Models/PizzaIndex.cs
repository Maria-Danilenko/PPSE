using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Models;
using WebApplication4.Services;

namespace WebApplication4.Pages
{
    public class PizzaIndex : PageModel
    {
        private readonly IPizzaService _pizzaService;

        public PizzaIndex(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        public IEnumerable<Pizza> Pizzas { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Pizzas = await _pizzaService.GetAllPizzasAsync();
            return Page();
        }
    }
}