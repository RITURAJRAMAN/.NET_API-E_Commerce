using Ecommerce.API.DataContext;
using Ecommerce.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.API.Services
{
    public class EcomAPIServices
    {
        private readonly AppDbContext _context;
        private readonly jwtOptions _options;

        public EcomAPIServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductsData>> GetProductData()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task AddProducts(ProductsData prodata)
        {
            _context.Products.Add(prodata);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var prodata = _context.Products.Find(id);
            if (prodata != null)
            {
                _context.Products.Remove(prodata);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditProduct(int id, ProductsData prodata)
        {
            ProductsData data = await _context.Products.FindAsync(id);
            if (data != null)
            {
                data.Product=prodata.Product;
                data.Quantity = prodata.Quantity;
                data.Price = prodata.Price;
                data.description = prodata.description;
                data.imageurl = prodata.imageurl;
                await _context.SaveChangesAsync();
            }

        }

        public async Task<ProductsData> GetProductbyId(int id)
        {
            ProductsData productsData = await _context.Products.FindAsync(id);
            return productsData;
        }

        public async Task<ProductsData> GetProductbyName(string name)
        {
            var product = await _context.Products.FirstOrDefaultAsync(u => u.Product == name);
            if (product!=null)
            {
                return product;
            }
            return null;
        }




        public async Task<List<Cartdata>> GetCartsData()
        {
            return await _context.Carts.ToListAsync();
        }

        public async Task AddCarts(Cartdata prodata)
        {
            _context.Carts.Add(prodata);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCarts(int id)
        {
            var prodata = _context.Carts.Find(id);
            if (prodata != null)
            {
                _context.Carts.Remove(prodata);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditCart(int id, Cartdata prodata)
        {
            Cartdata data = await _context.Carts.FindAsync(id);
            if (data != null)
            {
                data.Quantity = prodata.Quantity;
                await _context.SaveChangesAsync();
            }

        }




        public async Task<List<OrdersData>> GetOrdersData()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task AddOrders(OrdersData prodata)
        {
            _context.Orders.Add(prodata);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrders(int id)
        {
            var prodata = _context.Orders.Find(id);
            if (prodata != null)
            {
                _context.Orders.Remove(prodata);
                await _context.SaveChangesAsync();
            }
        }




        public async Task<List<UsersData>> GetUserData()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddUsers(UsersData userdata)
        {
            
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(userdata.Password));
            userdata.Password = Convert.ToBase64String(hash);
            _context.Users.Add(userdata);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckCredentials(loginCred usersdata)
        {
            var cred = await _context.Users.Where(x=>x.Email == usersdata.Email).FirstOrDefaultAsync();
            var pass = cred.Password;
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(usersdata.Password));
            usersdata.Password = Convert.ToBase64String(hash);
            if (pass != usersdata.Password)
            {
                return false;
            }
            return true;
        }

        public async Task DeleteUser(int id)
        {
            var userdata = _context.Users.Find(id);
            if (userdata != null)
            {
                _context.Users.Remove(userdata);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditUser(int id, UsersData userdata)
        {
            UsersData data = await _context.Users.FindAsync(id);
            if (data != null)
            {
                data.UserName = userdata.UserName;
                data.Password = userdata.Password;
                data.Email = userdata.Email;
                data.Phone = userdata.Phone;
                data.age = userdata.age;

                await _context.SaveChangesAsync();
            }

        }

        public async Task<UsersData> GetUserbyId(int id)
        {
            UsersData userData = await _context.Users.FindAsync(id);
            return userData;
        }

        public async Task<UsersData> GetUserByEmail(string email)
        {     
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
}
