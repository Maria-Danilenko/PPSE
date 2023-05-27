using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;
using WebApplication4.DataContext;

namespace WebApplication4.Services
{
    public interface IPizzaService
    {
        Task<IEnumerable<Pizza>> GetAllPizzasAsync();
        Task<Pizza> GetPizzaByIdAsync(int id);
        Task AddPizzaAsync(Pizza pizza);
        Task UpdatePizzaAsync(Pizza pizza);
        Task DeletePizzaAsync(int id);
    }
    public class PizzaService : IPizzaService
    {
        private readonly PizzasContext _context;

        public PizzaService(PizzasContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pizza>> GetAllPizzasAsync()
        {
            return await _context.Pizzas.ToListAsync();
        }

        public async Task<Pizza> GetPizzaByIdAsync(int id)
        {
            return await _context.Pizzas.FindAsync(id);
        }

        public async Task AddPizzaAsync(Pizza pizza)
        {
            _context.Pizzas.Add(pizza);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePizzaAsync(Pizza pizza)
        {
            _context.Pizzas.Update(pizza);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePizzaAsync(int id)
        {
            var pizza = await _context.Pizzas.FindAsync(id);
            if (pizza != null)
            {
                _context.Pizzas.Remove(pizza);
                await _context.SaveChangesAsync();
            }
        }
    }
}

