namespace WebApplication4.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserAddress { get; set; }
        public double Price { get; set; }
    }
}
