namespace Ecommerce.API.Models
{
    public class OrdersData
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string imageurl { get; set; }
        public DateTime CreatedDate { get; set; }

        public OrdersData() { 
            CreatedDate = DateTime.Today;
        }

    }
}
