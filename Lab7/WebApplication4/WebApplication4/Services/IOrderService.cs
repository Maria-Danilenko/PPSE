using WebApplication4.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;

namespace WebApplication4.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
    }

    public class OrderService : IOrderService
    {
        private readonly List<Order> _orders;
        public OrderService(IUserService userService, IPizzaService pizzaService)
        {           
            _orders = new List<Order>
            {
                   new Order() { Id = 1, UserId = 1, UserAddress = userService.GetUserByIdAsync(1).Result.Address,
                       Price = pizzaService.GetPizzaByIdAsync(1).Result.Price + pizzaService.GetPizzaByIdAsync(2).Result.Price},
                   new Order() { Id = 2, UserId = 2, UserAddress = userService.GetUserByIdAsync(2).Result.Address,
                       Price = pizzaService.GetPizzaByIdAsync(2).Result.Price + pizzaService.GetPizzaByIdAsync(3).Result.Price},
                   new Order() { Id = 3, UserId = 3, UserAddress = userService.GetUserByIdAsync(3).Result.Address,
                       Price = pizzaService.GetPizzaByIdAsync(3).Result.Price},
                   new Order() { Id = 4, UserId = 4, UserAddress = userService.GetUserByIdAsync(4).Result.Address,
                       Price = pizzaService.GetPizzaByIdAsync(4).Result.Price * 2},
                   new Order() { Id = 5, UserId = 5, UserAddress = userService.GetUserByIdAsync(5).Result.Address,
                       Price = pizzaService.GetPizzaByIdAsync(5).Result.Price + pizzaService.GetPizzaByIdAsync(6).Result.Price},
                   new Order() { Id = 6, UserId = 6, UserAddress = userService.GetUserByIdAsync(6).Result.Address,
                       Price = pizzaService.GetPizzaByIdAsync(6).Result.Price * 2 + pizzaService.GetPizzaByIdAsync(7).Result.Price},
                   new Order() { Id = 7, UserId = 7, UserAddress = userService.GetUserByIdAsync(7).Result.Address,
                       Price = pizzaService.GetPizzaByIdAsync(7).Result.Price},
                   new Order() { Id = 8, UserId = 8, UserAddress = userService.GetUserByIdAsync(8).Result.Address,
                       Price = pizzaService.GetPizzaByIdAsync(8).Result.Price + pizzaService.GetPizzaByIdAsync(1).Result.Price},
                   new Order() { Id = 9, UserId = 9, UserAddress = userService.GetUserByIdAsync(9).Result.Address,
                       Price = pizzaService.GetPizzaByIdAsync(9).Result.Price * 3},
                   new Order() { Id = 10, UserId = 10, UserAddress = userService.GetUserByIdAsync(10).Result.Address,
                       Price = pizzaService.GetPizzaByIdAsync(10).Result.Price}
            };
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await Task.FromResult<IEnumerable<Order>>(_orders);
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await Task.FromResult(_orders.FirstOrDefault(o => o.Id == id));
        }
        public async Task AddOrderAsync(Order order)
        {
            order.Id = _orders.Count + 1;
            _orders.Add(order);
            await Task.CompletedTask;

        }
        public async Task UpdateOrderAsync(Order order)
        {
            var index = _orders.FindIndex(o => o.Id == order.Id);
            _orders[index] = order;
            await Task.CompletedTask;
        }
        public async Task DeleteOrderAsync(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                _orders.Remove(order);
            }
            await Task.CompletedTask;
        }
    }
}

