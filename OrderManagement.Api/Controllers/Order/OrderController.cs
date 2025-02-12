
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
        private readonly IOrderBusiness _orderBusiness;
        private readonly IOrderMessageBusiness _orderMessageBusiness;

        public OrderController(
            ILogger<OrderController> logger,
            IOrderBusiness orderBusiness,
            IOrderMessageBusiness orderMessageBusiness)
        {
            _logger = logger;
            _orderBusiness = orderBusiness;
            _orderMessageBusiness = orderMessageBusiness;
        }

        [HttpGet]
        [Route("")]
        public ActionResult List()
        {
            var orders = _orderBusiness.List();

            return Answer(new ResponseDto<PageDto<OrderDto>>
            {
                Entity = orders
            });
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(long id)
        {
            var order = _orderBusiness.Get(id);

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
            await _orderMessageBusiness.Add(dto);

            //empty response for success
            return Answer(new ResponseDto<BaseDto>());
        }

        [HttpPost]
        [Route("process")]
        public ActionResult Process(OrderDto dto)
        {
            var order = _orderBusiness.New(dto);

            return Answer(new ResponseDto<OrderDto>
            {
                Entity = order
            });
        }
    }
}
