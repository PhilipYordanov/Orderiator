using Microsoft.EntityFrameworkCore;
using Orderiator.DataModel.Models;

namespace Orderiator.DataModel
{
    public class OrderiatorDbContext : DbContext
    {
        public OrderiatorDbContext()
        {

        }

        public OrderiatorDbContext(DbContextOptions<OrderiatorDbContext> options)
            : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Product> Products { get; set; }
        
    }
}
