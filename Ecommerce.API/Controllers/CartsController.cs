using Ecommerce.API.Models;
using Ecommerce.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly EcomAPIServices _services;
        public CartsController(EcomAPIServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarts()
        {
            return Ok(await _services.GetCartsData());
        }

        [HttpPost]
        public async Task<IActionResult> AddCart([FromBody] Cartdata prodata)
        {
            if (prodata == null)
            {
                return BadRequest("Kuchh to input dalo!");
            }
            await _services.AddCarts(prodata);
            return Ok(new { message = "Cart Item Added!" });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCart([FromQuery] int id)
        {
            await _services.DeleteCarts(id);
            return Ok(new { message = "Cart Item Deleted!" });
        }

        [HttpPut]
        public async Task<IActionResult> EditCart(int id, [FromBody] Cartdata prodata)
        {
            if (prodata == null)
            {
                return BadRequest("Kya Change Karna hai Bhai!");
            }
            else
            {
                await _services.EditCart(id, prodata);
                return Ok(new { message = "Cart Updated!" });
            }
        }
    }
}
