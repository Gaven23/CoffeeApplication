using System.ComponentModel.DataAnnotations;

namespace CoffeeApplication.Data.Entities
{
    public class Order
    {
        public int OrderId { get; set; } 
        public string Id { get; set; }
        public int DiscountId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
        public Product Product { get; set; }
        public Discount Discount { get; set; }
    }
}
