using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderManagement.WebUI.Controllers.Order
{
    public class OrderController : Controller
    {
        [Route("order/list")]
        public IActionResult List()
        {
            return View();
        }

        [Route("order")]
        public IActionResult Get()
        {
            return View();
        }

        [Route("order/new")]
        public IActionResult New()
        {
            return View();
        }
    }
}
