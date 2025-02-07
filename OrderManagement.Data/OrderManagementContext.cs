
using Microsoft.EntityFrameworkCore;
using OrderManagement.Data.Entity.Orders;

namespace OrderManagement.Data
{
    public class OrderManagementContext : DbContext
    {
        public OrderManagementContext(DbContextOptions<OrderManagementContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
    }
}
