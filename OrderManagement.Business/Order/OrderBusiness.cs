
using OrderManagement.Data;
using OrderManagement.Model;
using OrderManagement.Model.Orders;

namespace OrderManagement.Business.Order
{
    public class OrderBusiness : BaseBusiness, IOrderBusiness
    {
        OrderManagementContext _context;

        public OrderBusiness(OrderManagementContext context)
        {
            _context = context;
        }

        public PageDto<OrderDto> List()
        {
            //no paging etc for demo
            var orders = _context.Orders.Select(a => new OrderDto
            {
                Id = a.Id,
                ProductName = a.ProductName,
                Price = a.Price,
                Status = a.Status
            }).ToList();

            return new PageDto<OrderDto>
            {
                Collection = orders
            };
        }

        public OrderDto? Get(long id)
        {
            var order = _context.Orders.FirstOrDefault(a => a.Id == id);
            if(order == null)
                return null;

            return new OrderDto
            {
                Id = order.Id,
                ProductName = order.ProductName,
                Price = order.Price,
                Status = order.Status
            };
        }

        public OrderDto New(OrderDto dto)
        {
            //no evaluation etc. here for demo
            var order = new Data.Entity.Orders.Order
            {
                ProductName = dto.ProductName ?? "-",
                Price = dto.Price ?? 0,
                Status = dto.Status ?? "Pending"
            };

            _context.Orders.Add(order);

            _context.SaveChanges();

            return new OrderDto { Id = order.Id };
        }
    }
}
