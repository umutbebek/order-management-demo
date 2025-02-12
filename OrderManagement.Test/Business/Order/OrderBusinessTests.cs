
using OrderManagement.Business.Order;
using OrderManagement.Model.Orders;
using Xunit;

namespace OrderManagement.Test.Business.Order
{
    public class OrderBusinessTests
    {
        private readonly IOrderBusiness _orderBusiness;

        public OrderBusinessTests()
        {
            _orderBusiness = new OrderBusiness();
        }
        [Fact]
        public void Get_NotExists()
        {
            OrderDto? expected = null;

            var actual = _orderBusiness.Get(long.MaxValue);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Get_Exists(int index)
        {
            var actual = _orderBusiness.List().Collection.Skip(0).Take(1).FirstOrDefault();

            var expected = _orderBusiness.Get(actual.Id ?? 0);

            Assert.Equal(expected.Id ?? 1, actual.Id ?? 2);
        }
    }
}
