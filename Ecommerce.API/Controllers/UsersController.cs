using Ecommerce.API.DataContext;
using Ecommerce.API.Models;
using Ecommerce.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly EcomAPIServices _services;
        private readonly jwtOptions _options;
        public UsersController(EcomAPIServices services, IOptions<jwtOptions> options)
        {
            _services = services;
            _options = options.Value;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            return Ok(await _services.GetUserData());
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UsersData userdata)
        {
            if (userdata == null)
            {
                return BadRequest("Kuchh to input dalo!");
            }
            await _services.AddUsers(userdata);
            return Ok(new { message = "User Added!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] loginCred user)
        {
            var res = await _services.CheckCredentials(user);

            if (res)
            {

                var userdata = await _services.GetUserByEmail(user.Email);

                var jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
                var crendential = new SigningCredentials(jwtKey, SecurityAlgorithms.HmacSha256);
                List<Claim> claims = new List<Claim>()
                {
                    new Claim("Email",userdata.Email),
                    new Claim("Role",userdata.role)
                };
                var sToken = new JwtSecurityToken(_options.Key, _options.Issuer, claims, expires: DateTime.Now.AddHours(5), signingCredentials: crendential);
                var token = new JwtSecurityTokenHandler().WriteToken(sToken);
                return Ok(new { token = token });
            }
            else
            {
                return NotFound(new { message = "Incorrect Password!" });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromQuery] int id)
        {
            await _services.DeleteUser(id);
            return Ok(new { message = "User Deleted!" });
        }

        [HttpPut]
        public async Task<IActionResult> EditUser(int id, [FromBody] UsersData userdata)
        {
            if (userdata == null)
            {
                return BadRequest("Kya Change Karna hai Bhai!");
            }
            else
            {
                await _services.EditUser(id, userdata);
                return Ok(new { message = "User Edited!" });
            }
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetUserbyId(int id)
        //{
        //    return Ok(await _services.GetUserbyId(id));
        //}

        [HttpGet("{email}")]
        public async Task<IActionResult>GetUserbyEmail(string email)
        {
            return Ok(await _services.GetUserByEmail(email));
        }
    }
}
