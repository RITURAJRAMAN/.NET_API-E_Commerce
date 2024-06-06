using Ecommerce.API.Models;
using Ecommerce.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly EcomAPIServices _services;
        public OrdersController(EcomAPIServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await _services.GetOrdersData());
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrdersData prodata)
        {
            if (prodata == null)
            {
                return BadRequest("Kuchh to input dalo!");
            }
            await _services.AddOrders(prodata);
            return Ok(new { message = "Order Item Added!" });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrder([FromQuery] int id)
        {
            await _services.DeleteOrders(id);
            return Ok(new { message = "Order Deleted!" });
        }
    }
}
