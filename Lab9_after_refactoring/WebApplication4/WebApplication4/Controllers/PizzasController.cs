using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using WebApplication4.Models;
using WebApplication4.Services;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication4.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PizzasController : ControllerBase
    {
        private readonly IPizzaService _pizzaService;

        public PizzasController(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pizza>>> Get()
        {
            var pizzas = await _pizzaService.GetAllPizzasAsync();
            return Ok(pizzas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pizza>> GetById(int id)
        {
            var pizza = await _pizzaService.GetPizzaByIdAsync(id);

            if (pizza == null)
            {
                return NotFound();
            }

            return Ok(pizza);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pizza pizza)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _pizzaService.AddPizzaAsync(pizza);
            return CreatedAtAction(nameof(GetById), new { id = pizza.Id }, pizza);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Pizza pizza)
        {
            if (id != pizza.Id)
            {
                return BadRequest();
            }

            await _pizzaService.UpdatePizzaAsync(pizza);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _pizzaService.DeletePizzaAsync(id);
            return NoContent();
        }     
    }
}
