using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagement.Business.Order;
using OrderManagement.Model.Orders;
using Xunit;

namespace OrderManagement.Test.Business.Order
{
    public class OrderBusinessTests
    {
        [Fact]
        public void Get_NotExists()
        {
            OrderDto? expected = null;

            var business = new OrderBusiness();
            var actual = business.Get(long.MaxValue);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Get_Exists(int index)
        {
            var business = new OrderBusiness();
            var actual = business.List().Collection.Skip(0).Take(1).FirstOrDefault();

            var expected = business.Get(actual.Id ?? 0);

            Assert.Equal(expected.Id ?? 1, actual.Id ?? 2);
        }
    }
}
