﻿using WebApplication4.Models;
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
        public UserService()
        {
            _users = new List<User>
            {
                    new User() { Id = 1, Name = "Camilla", Surname = "Stuart", PhoneNumber = "(397) 739-7219",
                        Address = "286 St Louis Road, Poughkeepsie, NY 12601" },
                    new User() { Id = 2, Name = "Philippa", Surname = "Cook", PhoneNumber = "(212) 788-5543",
                        Address = "938 Chapel Ave., Marlborough, MA 01752" },
                    new User() { Id = 3, Name = "Arran", Surname = "Mcgee", PhoneNumber = "(488) 891-3362",
                        Address = "560 N. Pumpkin Hill Lane, Mankato, MN 56001" },
                    new User() { Id = 4, Name = "Zaid", Surname = "Haines", PhoneNumber = "(334) 797-3463",
                        Address = "603 Ridgeview Ave., North Tonawanda, NY 14120" },
                    new User() { Id = 5, Name = "Aleksander", Surname = "Richard", PhoneNumber = "(536) 835-8378",
                        Address = "8653 Bedford Lane, Kaukauna, WI 54130" },
                    new User() { Id = 6, Name = "Lisa", Surname = "Osborn", PhoneNumber = "(213) 704-3278",
                        Address = "7064 Circle Street, Palm Bay, FL 32907" },
                    new User() { Id = 7, Name = "Teddy", Surname = "Molina", PhoneNumber = "(859) 223-3470",
                        Address = "7928 Division Street, Hanover, PA 17331" },
                    new User() { Id = 8, Name = "Denis", Surname = "Riddle", PhoneNumber = "(961) 859-7378",
                        Address = "9695 Big Rock Cove St., Fairfield, CT 06824" },
                    new User() { Id = 9, Name = "Husna", Surname = "Thomson", PhoneNumber = "(429) 717-0204",
                        Address = "3 Halifax Court, Brandon, FL 33510" },
                    new User() { Id = 10, Name = "Lorenzo", Surname = "Harrington", PhoneNumber = "(442) 635-1666",
                        Address = "772 Lafayette St., Batavia, OH 45103" }
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

