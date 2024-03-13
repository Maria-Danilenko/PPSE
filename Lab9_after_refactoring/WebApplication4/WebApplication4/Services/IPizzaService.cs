using WebApplication4.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;

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
        private readonly List<Pizza> _pizzas;
        public PizzaService()
        {
            _pizzas = new List<Pizza>
            {
                new Pizza() { Id = 1, Name = "Margherita", Description = "Tomato sauce, mozzarella cheese, and fresh basil.", Price = 8.99 },
                new Pizza() { Id = 2, Name = "Pepperoni", Description = "Tomato sauce, mozzarella cheese, and pepperoni.", Price = 10.99 },
                new Pizza() { Id = 3, Name = "Vegetarian", Description = "Tomato sauce, mozzarella cheese, mushrooms, green peppers, and onions.", Price = 9.99 },
                new Pizza() { Id = 4, Name = "Meat Lovers", Description = "Tomato sauce, mozzarella cheese, pepperoni, bacon, sausage, and ham.", Price = 12.99 },
                new Pizza() { Id = 5, Name = "Hawaiian", Description = "Tomato sauce, mozzarella cheese, ham, and pineapple.", Price = 11.99 },
                new Pizza() { Id = 6, Name = "BBQ Chicken", Description = "BBQ sauce, mozzarella cheese, grilled chicken, red onions, and cilantro.", Price = 13.99 },
                new Pizza() { Id = 7, Name = "Supreme", Description = "Tomato sauce, mozzarella cheese, pepperoni, sausage, green peppers, onions, and mushrooms.", Price = 14.99 },
                new Pizza() { Id = 8, Name = "Buffalo Chicken", Description = "Buffalo sauce, mozzarella cheese, grilled chicken, red onions, and ranch dressing.", Price = 12.99 },
                new Pizza() { Id = 9, Name = "Cheese", Description = "Tomato sauce and mozzarella cheese.", Price = 7.99 },
                new Pizza() { Id = 10, Name = "Mediterranean", Description = "Pesto sauce, mozzarella cheese, feta cheese, artichoke hearts, kalamata olives, and sun-dried tomatoes.", Price = 13.99 }
            };
        }
        public async Task<IEnumerable<Pizza>> GetAllPizzasAsync()
        {
            return await Task.FromResult<IEnumerable<Pizza>>(_pizzas);
        }
        public async Task<Pizza> GetPizzaByIdAsync(int id)
        {

            return await Task.FromResult(_pizzas.FirstOrDefault(p => p.Id == id));
        }
        public async Task AddPizzaAsync(Pizza pizza)
        {
            pizza.Id = _pizzas.Count + 1;
            _pizzas.Add(pizza);
            await Task.CompletedTask;

        }
        public async Task UpdatePizzaAsync(Pizza pizza)
        {
            var index = _pizzas.FindIndex(p => p.Id == pizza.Id);
            _pizzas[index] = pizza;
            await Task.CompletedTask;
        }
        public async Task DeletePizzaAsync(int id)
        {
            var pizza = _pizzas.FirstOrDefault(p => p.Id == id);
            if (pizza != null)
            {
                _pizzas.Remove(pizza);
            }
            await Task.CompletedTask;
        }
    }
}

