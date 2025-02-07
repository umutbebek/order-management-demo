using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using OrderManagement.Model;

namespace OrderManagement.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Returns the data as json
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ContentResult Answer<T>(ResponseDto<T> data) where T : BaseDto
        {
            return Content(JsonConvert.SerializeObject(data), "application/json");
        }
    }
}
