using Ecommerce.API.Models;
using Ecommerce.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly EcomAPIServices _services;
        public ProductsController(EcomAPIServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _services.GetProductData());
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductsData prodata)
        {
            if (prodata == null)
            {
                return BadRequest("Kuchh to input dalo!");
            }
            await _services.AddProducts(prodata);
            return Ok(new { message = "Product Added!" });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromQuery] int id)
        {
            await _services.DeleteProduct(id);
            return Ok(new { message = "Product Deleted!" });
        }

        [HttpPut]
        public async Task<IActionResult> EditProduct(int id, [FromBody] ProductsData prodata)
        {
            if (prodata == null)
            {
                return BadRequest("Kya Change Karna hai Bhai!");
            }
            else
            {
                await _services.EditProduct(id, prodata);
                return Ok(new { message = "Product Edited!" });
            }
        }

        [HttpGet("{product}")]
        public async Task<IActionResult> GetProductbyId(string product)
        {
            return Ok(await _services.GetProductbyName(product));
        }
    }
}
