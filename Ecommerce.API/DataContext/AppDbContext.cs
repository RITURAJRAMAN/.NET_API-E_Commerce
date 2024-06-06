using Ecommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.DataContext
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Cartdata> Carts { get; set; }
        public DbSet<UsersData> Users { get; set; }
        public DbSet<ProductsData> Products { get; set; }
        public DbSet<OrdersData> Orders { get; set; }
    }
}
