
using OrderManagement.Model.Orders;

namespace OrderManagement.Business.Order
{
    public interface IOrderMessageBusiness
    {
        Task Add(OrderDto dto);
    }
}