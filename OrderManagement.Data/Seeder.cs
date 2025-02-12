
using OrderManagement.Data.Entity.Orders;

namespace OrderManagement.Data
{
    public static class Seeder
    {
        public static void Seed(OrderManagementContext context)
        {
            context.Orders.Add(new Order
            {
                ProductName = "Product 1",
                Price = 12.15m,
                Status = "Approved"
            });
            context.Orders.Add(new Order
            {
                ProductName = "Product 2",
                Price = 32.60m,
                Status = "Rejected"
            });
            context.Orders.Add(new Order
            {
                ProductName = "Product 3",
                Price = 25.00m,
                Status = "Approved"
            });
            context.SaveChanges();
        }
    }
}
