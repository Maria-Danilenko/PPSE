using WebApplication4.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;

namespace WebApplication4.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }

    public class UserService : IUserService
    {
        private readonly List<User> _users;
        public UserService(IUserAuthService userAuthService)
        {
            _users = new List<User>
            {
                    new User() { Id = 1, Name = userAuthService.GetUserAuthByIdAsync(1).Result.FirstName, Surname = userAuthService.GetUserAuthByIdAsync(1).Result.LastName, 
                        PhoneNumber = "(397) 739-7219", Address = "286 St Louis Road, Poughkeepsie, NY 12601" },
                    new User() { Id = 2, Name = userAuthService.GetUserAuthByIdAsync(2).Result.FirstName, Surname = userAuthService.GetUserAuthByIdAsync(2).Result.LastName,
                        PhoneNumber = "(212) 788-5543", Address = "938 Chapel Ave., Marlborough, MA 01752" },
                    new User() { Id = 3, Name = userAuthService.GetUserAuthByIdAsync(3).Result.FirstName, Surname = userAuthService.GetUserAuthByIdAsync(3).Result.LastName, 
                        PhoneNumber = "(488) 891-3362", Address = "560 N. Pumpkin Hill Lane, Mankato, MN 56001" },
                    new User() { Id = 4, Name = userAuthService.GetUserAuthByIdAsync(4).Result.FirstName, Surname = userAuthService.GetUserAuthByIdAsync(4).Result.LastName, 
                        PhoneNumber = "(334) 797-3463", Address = "603 Ridgeview Ave., North Tonawanda, NY 14120" },
                    new User() { Id = 5, Name = userAuthService.GetUserAuthByIdAsync(5).Result.FirstName, Surname = userAuthService.GetUserAuthByIdAsync(5).Result.LastName, 
                        PhoneNumber = "(536) 835-8378", Address = "8653 Bedford Lane, Kaukauna, WI 54130" }
            };
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await Task.FromResult<IEnumerable<User>>(_users);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
        }
        public async Task AddUserAsync(User user)
        {
            user.Id = _users.Count + 1;
            _users.Add(user);
            await Task.CompletedTask;

        }
        public async Task UpdateUserAsync(User user)
        {
            var index = _users.FindIndex(u => u.Id == user.Id);
            _users[index] = user;
            await Task.CompletedTask;
        }
        public async Task DeleteUserAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _users.Remove(user);
            }
            await Task.CompletedTask;
        }
    }
}

