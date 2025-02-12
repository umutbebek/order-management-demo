
using System.Text;
using OrderManagement.Model.Orders;
using RabbitMQ.Client;

namespace OrderManagement.Business.Order
{
    public class OrderMessageBusiness : BaseMessageBusiness, IOrderMessageBusiness
    {
        public async Task Add(OrderDto dto)
        {
            await Send(dto);
        }
    }
}