
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Business.Order;
using OrderManagement.Model;
using OrderManagement.Model.Orders;

namespace OrderManagement.Api.Controllers.Order
{
    [ApiController]
    [Route("v1/order")]
    public class OrderController : BaseController
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public ActionResult List()
        {
            var business = new OrderBusiness();
            var orders = business.List();

            return Answer(new ResponseDto<PageDto<OrderDto>>
            {
                Entity = orders
            });
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(long id)
        {
            var business = new OrderBusiness();
            var order = business.Get(id);

            return Answer(new ResponseDto<OrderDto>
            {
                HasError = order == null ? true : null,
                Error = order == null ? "Can not find the order!" : null,
                Entity = order
            });
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> New(OrderDto dto)
        {
            var business = new OrderMessageBusiness();
            await business.Add(dto);

            //empty response for success
            return Answer(new ResponseDto<BaseDto>());
        }

        [HttpPost]
        [Route("process")]
        public ActionResult Process(OrderDto dto)
        {
            var business = new OrderBusiness();
            var order = business.New(dto);

            return Answer(new ResponseDto<OrderDto>
            {
                Entity = order
            });
        }
    }
}
