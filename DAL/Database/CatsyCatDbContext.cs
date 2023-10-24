using DomainEntities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Database
{
    public class CatsyCatDbContext : DbContext
    {
        public CatsyCatDbContext(DbContextOptions<CatsyCatDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProducts> OrderProduct { get; set; }
    }
}
