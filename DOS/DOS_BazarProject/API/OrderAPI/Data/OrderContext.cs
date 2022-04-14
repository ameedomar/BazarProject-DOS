using Microsoft.EntityFrameworkCore;
using OrderAPI.Model;

namespace OrderAPI.Data
{
    public class OrderContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public OrderContext(DbContextOptions<OrderContext> opt) : base(opt)
        {
        }
        
    }
}