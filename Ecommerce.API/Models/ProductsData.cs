namespace Ecommerce.API.Models
{
    public class ProductsData
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string imageurl { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public DateTime CreatedDate { get; set; }
        public ProductsData() {
            CreatedDate = DateTime.Today;
        }
    }
}
