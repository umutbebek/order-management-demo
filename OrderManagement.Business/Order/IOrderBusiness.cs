
using OrderManagement.Model;
using OrderManagement.Model.Orders;

namespace OrderManagement.Business.Order
{
    public interface IOrderBusiness
    {
        PageDto<OrderDto> List();

        OrderDto? Get(long id);

        OrderDto New(OrderDto dto);
    }
}
