using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.DataContext
{
    public class PizzasContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; } = null!;
        public PizzasContext(DbContextOptions<PizzasContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
