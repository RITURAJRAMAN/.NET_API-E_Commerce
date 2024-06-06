namespace Ecommerce.API.Models
{
    public class UsersData
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string role { get; set; }
        public int age { get; set; }
        public DateTime Created { get; set; }

        public UsersData() {
            Created = DateTime.Today;
        }
    }
}
